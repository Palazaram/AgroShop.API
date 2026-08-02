using AgroShop.Application.Dto.Common;
using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Entities;
using AgroShop.Core.Enums;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AgroShop.Application.Services
{
    public class ProductService : IProductService
    {
        private const string ImagesSubfolder = "products";

        // Backstop only - Add/Update/Delete below invalidate this explicitly.
        private const string ProductsCacheKey = "products:all";
        private static readonly TimeSpan ProductsCacheDuration = TimeSpan.FromMinutes(15);

        private const int DefaultPageSize = 20;
        private const int MaxPageSize = 100;

        private readonly IProductRepository _productRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAttributeOptionRepository _attributeOptionRepository;
        private readonly IProductAttributeRepository _productAttributeRepository;
        private readonly IProductAttributeValueRepository _productAttributeValueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _imageStorageService;
        private readonly ICacheService _cacheService;

        public ProductService(
            IProductRepository productRepository,
            ISubCategoryRepository subCategoryRepository,
            ISupplierRepository supplierRepository,
            IAttributeOptionRepository attributeOptionRepository,
            IProductAttributeRepository productAttributeRepository,
            IProductAttributeValueRepository productAttributeValueRepository,
            IUnitOfWork unitOfWork,
            IImageStorageService imageStorageService,
            ICacheService cacheService)
        {
            _productRepository = productRepository;
            _subCategoryRepository = subCategoryRepository;
            _supplierRepository = supplierRepository;
            _attributeOptionRepository = attributeOptionRepository;
            _productAttributeRepository = productAttributeRepository;
            _productAttributeValueRepository = productAttributeValueRepository;
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;
            _cacheService = cacheService;
        }

        public async Task<Result<PagedResult<ProductDto>, Error>> GetProductsAsync(
            bool asNoTracking = false,
            IEnumerable<Guid>? subCategoryIds = null,
            IEnumerable<Guid>? attributeOptionIds = null,
            IEnumerable<Guid>? supplierIds = null,
            int page = 1,
            int pageSize = DefaultPageSize,
            ProductSortBy sortBy = ProductSortBy.NameAsc,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Clamped here, not just at the controller - this is the one
            // place every caller (including future ones) actually goes
            // through, so it's the only place that has to be trusted.
            page = Math.Max(1, page);
            pageSize = Math.Clamp(pageSize, 1, MaxPageSize);

            var subCategoryIdSet = subCategoryIds?.Distinct().ToHashSet() ?? [];
            var optionIds = attributeOptionIds?.Distinct().ToList() ?? [];
            var supplierIdSet = supplierIds?.Distinct().ToHashSet() ?? [];
            var isFiltered = subCategoryIdSet.Count > 0 || optionIds.Count > 0 || supplierIdSet.Count > 0;

            if (!isFiltered)
            {
                // The cache holds the full, sorted, unpaginated list - paging
                // it happens in memory below instead of re-querying per page.
                // That's still a real win: it's slicing a list already in
                // the cache, not asking Postgres to redo the same scan+sort
                // on every page request for the common "no filters" case.
                var allProductDtos = await _cacheService.GetAsync<List<ProductDto>>(ProductsCacheKey, cancellationToken);

                if (allProductDtos == null)
                {
                    var allProducts = await _productRepository.GetProductsQueryable(asNoTracking)
                        .OrderBy(p => p.Name.Value)
                        .ToListAsync(cancellationToken);

                    allProductDtos = allProducts.ToDto().ToList();
                    await _cacheService.SetAsync(ProductsCacheKey, allProductDtos, ProductsCacheDuration, cancellationToken);
                }

                // The cache itself is only ever built sorted by name (see
                // above) - re-sorting the already-fetched list in memory here
                // is cheap and keeps every sortBy option working without a
                // separate cache entry per sort order.
                var sortedFromCache = SortProductDtos(allProductDtos, sortBy);
                var pagedFromCache = sortedFromCache.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                return Result.Success<PagedResult<ProductDto>, Error>(new PagedResult<ProductDto>
                {
                    Items = pagedFromCache,
                    TotalCount = allProductDtos.Count,
                    Page = page,
                    PageSize = pageSize,
                });
            }

            // Filtered results aren't cached under ProductsCacheKey - the
            // combinations of subCategoryIds + selected options are too varied
            // to key sensibly, and this path is already excluded from the
            // cache invalidated by Add/Update/Delete above.
            var productsQuery = _productRepository.GetProductsQueryable(asNoTracking);

            // OR between selected subcategories - a category maps to several
            // subcategories, so "all products in this category" is passing
            // all of them at once, same checkbox-facet semantics as the two
            // filters below.
            if (subCategoryIdSet.Count > 0)
                productsQuery = productsQuery.Where(p => subCategoryIdSet.Contains(p.SubCategoryId));

            // OR between selected suppliers, same checkbox-facet semantics as attributeOptionIds.
            if (supplierIdSet.Count > 0)
                productsQuery = productsQuery.Where(p => supplierIdSet.Contains(p.SupplierId));

            if (optionIds.Count > 0)
            {
                var allOptions = await _attributeOptionRepository.GetAttributeOptionsAsync(asNoTracking: true, cancellationToken);
                var optionsById = allOptions.ToDictionary(o => o.Id);

                // Same faceted-search semantics as the reference site: OR
                // between options of the same attribute ("Томат" or "Картопля"),
                // AND across different attributes (must also match "До сходів").
                var optionGroupsByAttributeId = optionIds
                    .Where(optionsById.ContainsKey)
                    .GroupBy(id => optionsById[id].AttributeId)
                    .Select(g => g.ToHashSet())
                    .ToList();

                // Chained .Where calls, not one .All() over the captured
                // list - each individual predicate below is simple enough
                // for EF to translate reliably; a single .All(group => ...)
                // wrapping a captured List<HashSet<Guid>> is exactly the
                // kind of shape that risks silently falling back to
                // client-side evaluation instead of a SQL WHERE.
                foreach (var group in optionGroupsByAttributeId)
                    productsQuery = productsQuery
                        .Where(p => p.ProductAttributeValues
                        .Any(pav => group.Contains(pav.AttributeOptionId)));
            }

            // Count after filters, before Skip/Take - this is a second round
            // trip to the DB (there's no way to get a filtered count and a
            // page of rows out of one query), but it's the standard shape
            // for offset pagination and the DTO needs TotalCount regardless.
            var totalCount = await productsQuery.CountAsync(cancellationToken);

            var pagedProducts = await SortProducts(productsQuery, sortBy)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            var filteredProductDtos = pagedProducts.ToDto().ToList();
            return Result.Success<PagedResult<ProductDto>, Error>(new PagedResult<ProductDto>
            {
                Items = filteredProductDtos,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
            });
        }

        private static IOrderedQueryable<Product> SortProducts(IQueryable<Product> query, ProductSortBy sortBy) =>
            sortBy switch
            {
                ProductSortBy.PriceAsc => query.OrderBy(p => p.Price.Value),
                ProductSortBy.PriceDesc => query.OrderByDescending(p => p.Price.Value),
                _ => query.OrderBy(p => p.Name.Value),
            };

        private static List<ProductDto> SortProductDtos(List<ProductDto> products, ProductSortBy sortBy) =>
            sortBy switch
            {
                ProductSortBy.PriceAsc => products.OrderBy(p => p.Price).ToList(),
                ProductSortBy.PriceDesc => products.OrderByDescending(p => p.Price).ToList(),
                _ => products.OrderBy(p => p.Name).ToList(),
            };

        public async Task<Result<ProductDto, Error>> GetProductByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var productId))
                return Result.Failure<ProductDto, Error>(Errors.General.IncorrectGuidError());

            var product = await _productRepository.GetProductByIdAsync(productId, asNoTracking, cancellationToken);

            if (product == null)
                return Result.Failure<ProductDto, Error>(Errors.Product.ProductIsNullById());

            return Result.Success<ProductDto, Error>(product.ToDto());
        }

        public async Task<UnitResult<Error>> AddAsync(AddProductDto productDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(productDto.SubCategoryId, cancellationToken: cancellationToken);
            if (subCategory == null)
                return UnitResult.Failure(Errors.SubCategory.SubCategoryIsNullById());

            var supplier = await _supplierRepository.GetSupplierByIdAsync(productDto.SupplierId, cancellationToken: cancellationToken);
            if (supplier == null)
                return UnitResult.Failure(Errors.Supplier.SupplierIsNullById());

            var selectionResult = await ValidateAttributeSelectionsAsync(productDto.SubCategoryId, productDto.AttributeOptionIds, cancellationToken);
            if (selectionResult.IsFailure)
                return UnitResult.Failure(selectionResult.Error);

            var imagePath = await _imageStorageService.SaveAsync(productDto.Image, ImagesSubfolder, cancellationToken);

            var productResult = Product.Create(
                productDto.Name,
                productDto.Description,
                productDto.Price,
                productDto.Sku,
                productDto.StockQuantity,
                productDto.PackageAmount,
                productDto.PackageUnit,
                productDto.SubCategoryId,
                productDto.SupplierId,
                imagePath);

            if (productResult.IsFailure)
                return UnitResult.Failure(productResult.Error);

            var product = productResult.Value;
            await _productRepository.AddAsync(product, cancellationToken);

            foreach (var (productAttributeId, option) in selectionResult.Value)
            {
                var valueResult = ProductAttributeValue.Create(productAttributeId, product.Id, option.Id);
                if (valueResult.IsFailure)
                    return UnitResult.Failure(valueResult.Error);

                await _productAttributeValueRepository.AddAsync(valueResult.Value, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(ProductsCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        public async Task<Result<ProductDto, Error>> UpdateAsync(string id, UpdateProductDto productDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var productId))
                return Result.Failure<ProductDto, Error>(Errors.General.IncorrectGuidError());

            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken: cancellationToken);
            if (product == null)
                return Result.Failure<ProductDto, Error>(Errors.Product.ProductIsNullById());

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(productDto.SubCategoryId, cancellationToken: cancellationToken);
            if (subCategory == null)
                return Result.Failure<ProductDto, Error>(Errors.SubCategory.SubCategoryIsNullById());

            var supplier = await _supplierRepository.GetSupplierByIdAsync(productDto.SupplierId, cancellationToken: cancellationToken);
            if (supplier == null)
                return Result.Failure<ProductDto, Error>(Errors.Supplier.SupplierIsNullById());

            var selectionResult = await ValidateAttributeSelectionsAsync(productDto.SubCategoryId, productDto.AttributeOptionIds, cancellationToken);
            if (selectionResult.IsFailure)
                return Result.Failure<ProductDto, Error>(selectionResult.Error);

            var previousImagePath = product.ImagePath;

            string? imagePath = null;
            if (productDto.Image != null)
                imagePath = await _imageStorageService.SaveAsync(productDto.Image, ImagesSubfolder, cancellationToken);

            var updateResult = product.Update(
                productDto.Name,
                productDto.Description,
                productDto.Price,
                productDto.Sku,
                productDto.StockQuantity,
                productDto.PackageAmount,
                productDto.PackageUnit,
                productDto.SubCategoryId,
                productDto.SupplierId,
                productDto.IsActive,
                imagePath);

            if (updateResult.IsFailure)
                return Result.Failure<ProductDto, Error>(updateResult.Error);

            // Reconcile: no Update on ProductAttributeValue by design (see the
            // entity) - existing rows not in the new selection are removed,
            // rows for newly-selected options are added, matches left alone.
            var existingValues = await _productAttributeValueRepository.GetByProductIdAsync(productId, cancellationToken: cancellationToken);
            var selectedOptionIds = selectionResult.Value.Select(s => s.Option.Id).ToHashSet();

            foreach (var existingValue in existingValues)
            {
                if (!selectedOptionIds.Contains(existingValue.AttributeOptionId))
                    _productAttributeValueRepository.Delete(existingValue);
            }

            var existingOptionIds = existingValues.Select(v => v.AttributeOptionId).ToHashSet();
            foreach (var (productAttributeId, option) in selectionResult.Value)
            {
                if (existingOptionIds.Contains(option.Id))
                    continue;

                var valueResult = ProductAttributeValue.Create(productAttributeId, productId, option.Id);
                if (valueResult.IsFailure)
                    return Result.Failure<ProductDto, Error>(valueResult.Error);

                await _productAttributeValueRepository.AddAsync(valueResult.Value, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(ProductsCacheKey, cancellationToken);

            // Only drop the old file once the new one is safely persisted.
            if (imagePath != null)
                await _imageStorageService.DeleteAsync(previousImagePath, cancellationToken);

            // Built from `subCategory`/`supplier` (already fetched above) rather
            // than product.ToDto() - Update() nulls those nav properties when
            // the corresponding FK changes, so that path would throw here.
            return Result.Success<ProductDto, Error>(new ProductDto
            {
                Id = product.Id,
                Name = product.Name.Value,
                Description = product.Description.Value,
                Price = product.Price.Value,
                Sku = product.Sku.Value,
                StockQuantity = product.StockQuantity.Value,
                PackageAmount = product.PackageSize.Amount,
                PackageUnit = product.PackageSize.Unit.ToString(),
                IsActive = product.IsActive,
                IsAvailable = product.IsAvailable,
                ImagePath = product.ImagePath,
                CreatedAtUtc = product.CreatedAtUtc,
                UpdatedUtc = product.UpdatedUtc,
                SubCategoryId = product.SubCategoryId,
                SubCategoryName = subCategory.Name.Value,
                SupplierId = product.SupplierId,
                SupplierName = supplier.Name.Value,
                AttributeValues = selectionResult.Value.Select(s => new ProductAttributeValueDto
                {
                    AttributeOptionId = s.Option.Id,
                    AttributeId = s.Option.AttributeId,
                    AttributeName = s.Option.Attribute.Name.Value,
                    Value = s.Option.Value.Value
                }).ToList()
            });
        }

        public async Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var productId))
                return UnitResult.Failure(Errors.General.IncorrectGuidError());

            var product = await _productRepository.GetProductByIdAsync(productId, cancellationToken: cancellationToken);
            if (product == null)
                return UnitResult.Failure(Errors.Product.ProductIsNullById());

            _productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(ProductsCacheKey, cancellationToken);
            await _imageStorageService.DeleteAsync(product.ImagePath, cancellationToken);
            return UnitResult.Success<Error>();
        }

        // Not cached - derived from product data that changes on every
        // Add/Update/Delete, and there's no per-facet invalidation hook to
        // keep a cached version correct.
        //
        // Every facet below is computed by re-applying every filter EXCEPT
        // its own dimension ("self-exclude") - the standard faceted-search
        // rule, so a facet's counts answer "how many if I additionally
        // picked this", not "how many before I picked anything". Attribute
        // groups are deduplicated by Attribute, not by the ProductAttribute
        // wiring row, so the same attribute shared across several
        // subcategories (e.g. a packaging-format attribute wired to both
        // seeds and fertilizers) merges into one group with combined
        // counts instead of requiring exactly one subcategory to be active.
        public async Task<Result<ProductFacetsDto, Error>> GetProductFacetsAsync(
            IEnumerable<Guid>? subCategoryIds = null,
            IEnumerable<Guid>? attributeOptionIds = null,
            IEnumerable<Guid>? supplierIds = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subCategoryIdSet = subCategoryIds?.Distinct().ToHashSet() ?? [];
            var optionIds = attributeOptionIds?.Distinct().ToList() ?? [];
            var supplierIdSet = supplierIds?.Distinct().ToHashSet() ?? [];

            var allOptions = await _attributeOptionRepository.GetAttributeOptionsAsync(asNoTracking: true, cancellationToken);
            var optionsById = allOptions.ToDictionary(o => o.Id);

            // Same OR-within-attribute/AND-across-attributes grouping as GetProductsAsync.
            var selectedGroupsByAttributeId = optionIds
                .Where(optionsById.ContainsKey)
                .GroupBy(id => optionsById[id].AttributeId)
                .ToDictionary(g => g.Key, g => g.ToHashSet());

            var baseQuery = _productRepository.GetProductsQueryable(asNoTracking: true);

            IQueryable<Product> BuildQuery(bool includeSubCategory, bool includeSupplier, Guid? excludeAttributeId)
            {
                var query = baseQuery;

                if (includeSubCategory && subCategoryIdSet.Count > 0)
                    query = query.Where(p => subCategoryIdSet.Contains(p.SubCategoryId));

                if (includeSupplier && supplierIdSet.Count > 0)
                    query = query.Where(p => supplierIdSet.Contains(p.SupplierId));

                foreach (var (attributeId, group) in selectedGroupsByAttributeId)
                {
                    if (attributeId == excludeAttributeId)
                        continue;

                    query = query.Where(p => p.ProductAttributeValues.Any(pav => group.Contains(pav.AttributeOptionId)));
                }

                return query;
            }

            // SubCategory facet - ignores subCategoryIds itself, keeps supplier + attributes.
            var subCategoryOptions = await BuildQuery(includeSubCategory: false, includeSupplier: true, excludeAttributeId: null)
                .GroupBy(p => p.SubCategoryId)
                .Select(g => new ProductFilterSubCategoryOptionDto
                {
                    SubCategoryId = g.Key,
                    ProductCount = g.Select(p => p.Id).Distinct().Count(),
                })
                .ToListAsync(cancellationToken);

            // Supplier facet - ignores supplierIds itself, keeps subCategory + attributes.
            var supplierOptions = await BuildQuery(includeSubCategory: true, includeSupplier: false, excludeAttributeId: null)
                .GroupBy(p => p.SupplierId)
                .Select(g => new ProductFilterSupplierOptionDto
                {
                    SupplierId = g.Key,
                    Name = g.First().Supplier.Name.Value,
                    ProductCount = g.Select(p => p.Id).Distinct().Count(),
                })
                .OrderByDescending(o => o.ProductCount)
                .ToListAsync(cancellationToken);

            // Which attributes are even relevant: wired to any subcategory
            // currently in scope (or to any subcategory at all, if nothing's
            // scoped - matches how subCategoryIds being empty means
            // "everything" everywhere else in this service).
            var allProductAttributes = await _productAttributeRepository.GetProductAttributesAsync(asNoTracking: true, cancellationToken);
            var inScopeAttributes = allProductAttributes
                .Where(pa => subCategoryIdSet.Count == 0 || subCategoryIdSet.Contains(pa.SubCategoryId))
                .Select(pa => pa.Attribute)
                .DistinctBy(a => a.Id)
                .ToList();

            var attributeGroups = new List<ProductFilterGroupDto>();
            foreach (var attribute in inScopeAttributes)
            {
                // This one's self-exclusion is per-attribute: every OTHER
                // selected attribute group still applies, subCategory/
                // supplier still apply, only this attribute's own
                // selection is left out.
                var scopedQuery = BuildQuery(includeSubCategory: true, includeSupplier: true, excludeAttributeId: attribute.Id);

                var optionCounts = await scopedQuery
                    .SelectMany(p => p.ProductAttributeValues
                        .Where(pav => pav.AttributeOption.AttributeId == attribute.Id)
                        .Select(pav => new { pav.AttributeOptionId, ProductId = p.Id }))
                    .GroupBy(x => x.AttributeOptionId)
                    .Select(g => new { AttributeOptionId = g.Key, Count = g.Select(x => x.ProductId).Distinct().Count() })
                    .ToListAsync(cancellationToken);

                if (optionCounts.Count == 0)
                    continue;

                var options = optionCounts
                    .Select(oc => new ProductFilterOptionDto
                    {
                        AttributeOptionId = oc.AttributeOptionId,
                        Value = optionsById[oc.AttributeOptionId].Value.Value,
                        ProductCount = oc.Count,
                    })
                    .OrderByDescending(o => o.ProductCount)
                    .ToList();

                attributeGroups.Add(new ProductFilterGroupDto
                {
                    AttributeId = attribute.Id,
                    AttributeName = attribute.Name.Value,
                    ValueType = attribute.ValueType.ToString(),
                    Options = options,
                });
            }

            return Result.Success<ProductFacetsDto, Error>(new ProductFacetsDto
            {
                SubCategoryOptions = subCategoryOptions,
                SupplierOptions = supplierOptions,
                AttributeGroups = attributeGroups,
            });
        }

        // Validates that every selected option exists, belongs to an attribute
        // actually linked to this subcategory, respects SingleSelect's
        // one-value limit, and that every attribute configured for the
        // subcategory has at least one selected value (attribute values are
        // required - see the earlier decision). Returns each selection paired
        // with the ProductAttribute it satisfies, ready to persist.
        private async Task<Result<List<(Guid ProductAttributeId, AttributeOption Option)>, Error>> ValidateAttributeSelectionsAsync(
            Guid subCategoryId,
            IEnumerable<Guid> selectedOptionIds,
            CancellationToken cancellationToken)
        {
            var distinctOptionIds = selectedOptionIds.Distinct().ToList();

            var subCategoryAttributes = (await _productAttributeRepository.GetProductAttributesAsync(asNoTracking: true, cancellationToken))
                .Where(pa => pa.SubCategoryId == subCategoryId)
                .ToList();

            var allOptions = await _attributeOptionRepository.GetAttributeOptionsAsync(asNoTracking: true, cancellationToken);
            var optionsById = allOptions.ToDictionary(o => o.Id);

            var selections = new List<(Guid ProductAttributeId, AttributeOption Option)>();

            foreach (var optionId in distinctOptionIds)
            {
                if (!optionsById.TryGetValue(optionId, out var option))
                    return Result.Failure<List<(Guid, AttributeOption)>, Error>(Errors.AttributeOption.AttributeOptionIsNullById());

                var productAttribute = subCategoryAttributes.FirstOrDefault(pa => pa.AttributeId == option.AttributeId);
                if (productAttribute == null)
                    return Result.Failure<List<(Guid, AttributeOption)>, Error>(Errors.ProductAttributeValue.AttributeOptionDoesNotBelongToProductAttribute());

                selections.Add((productAttribute.Id, option));
            }

            var selectionsByProductAttributeId = selections.ToLookup(s => s.ProductAttributeId);

            foreach (var productAttribute in subCategoryAttributes)
            {
                var selectedForThisAttribute = selectionsByProductAttributeId[productAttribute.Id].ToList();

                if (selectedForThisAttribute.Count == 0)
                    return Result.Failure<List<(Guid, AttributeOption)>, Error>(
                        Errors.ProductAttributeValue.RequiredAttributeMissingValue(productAttribute.Attribute.Name.Value));

                if (productAttribute.Attribute.ValueType == AttributeValueType.SingleSelect && selectedForThisAttribute.Count > 1)
                    return Result.Failure<List<(Guid, AttributeOption)>, Error>(
                        Errors.ProductAttributeValue.TooManyValuesForSingleSelectAttribute(productAttribute.Attribute.Name.Value));
            }

            return Result.Success<List<(Guid, AttributeOption)>, Error>(selections);
        }
    }
}
