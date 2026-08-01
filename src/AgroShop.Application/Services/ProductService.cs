using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Entities;
using AgroShop.Core.Enums;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class ProductService : IProductService
    {
        private const string ImagesSubfolder = "products";

        // Backstop only - Add/Update/Delete below invalidate this explicitly.
        private const string ProductsCacheKey = "products:all";
        private static readonly TimeSpan ProductsCacheDuration = TimeSpan.FromMinutes(15);

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

        public async Task<Result<IEnumerable<ProductDto>, Error>> GetProductsAsync(
            bool asNoTracking = false,
            IEnumerable<Guid>? subCategoryIds = null,
            IEnumerable<Guid>? attributeOptionIds = null,
            IEnumerable<Guid>? supplierIds = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subCategoryIdSet = subCategoryIds?.Distinct().ToHashSet() ?? [];
            var optionIds = attributeOptionIds?.Distinct().ToList() ?? [];
            var supplierIdSet = supplierIds?.Distinct().ToHashSet() ?? [];
            var isFiltered = subCategoryIdSet.Count > 0 || optionIds.Count > 0 || supplierIdSet.Count > 0;

            if (!isFiltered)
            {
                var cached = await _cacheService.GetAsync<List<ProductDto>>(ProductsCacheKey, cancellationToken);
                if (cached != null)
                    return Result.Success<IEnumerable<ProductDto>, Error>(cached);

                var allProducts = await _productRepository.GetProductsAsync(asNoTracking, cancellationToken);
                var allProductDtos = allProducts.ToDto().OrderBy(p => p.Name).ToList();

                await _cacheService.SetAsync(ProductsCacheKey, allProductDtos, ProductsCacheDuration, cancellationToken);

                return Result.Success<IEnumerable<ProductDto>, Error>(allProductDtos);
            }

            // Filtered results aren't cached under ProductsCacheKey - the
            // combinations of subCategoryIds + selected options are too varied
            // to key sensibly, and this path is already excluded from the
            // cache invalidated by Add/Update/Delete above.
            var products = await _productRepository.GetProductsAsync(asNoTracking, cancellationToken);

            // OR between selected subcategories - a category maps to several
            // subcategories, so "all products in this category" is passing
            // all of them at once, same checkbox-facet semantics as the two
            // filters below.
            if (subCategoryIdSet.Count > 0)
                products = products.Where(p => subCategoryIdSet.Contains(p.SubCategoryId));

            // OR between selected suppliers, same checkbox-facet semantics as attributeOptionIds.
            if (supplierIdSet.Count > 0)
                products = products.Where(p => supplierIdSet.Contains(p.SupplierId));

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

                products = products.Where(p =>
                    optionGroupsByAttributeId.All(group =>
                        p.ProductAttributeValues.Any(pav => group.Contains(pav.AttributeOptionId))));
            }

            var filteredProductDtos = products.ToDto().OrderBy(p => p.Name).ToList();
            return Result.Success<IEnumerable<ProductDto>, Error>(filteredProductDtos);
        }

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

        // Not cached - it's derived from product data that changes on every
        // Add/Update/Delete, and there's no per-subcategory invalidation
        // hook yet to keep a cached version correct.
        public async Task<Result<ProductFiltersDto, Error>> GetFiltersBySubCategoryAsync(string subCategoryId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(subCategoryId, out var subCategoryGuid))
                return Result.Failure<ProductFiltersDto, Error>(Errors.General.IncorrectGuidError());

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(subCategoryGuid, asNoTracking: true, cancellationToken: cancellationToken);
            if (subCategory == null)
                return Result.Failure<ProductFiltersDto, Error>(Errors.SubCategory.SubCategoryIsNullById());

            var products = (await _productRepository.GetProductsAsync(asNoTracking: true, cancellationToken))
                .Where(p => p.SubCategoryId == subCategoryGuid)
                .ToList();

            // How many distinct products currently carry each supplier - same
            // "(81)" style counts as the attribute options below.
            var supplierOptions = products
                .GroupBy(p => p.SupplierId)
                .Select(g => new ProductFilterSupplierOptionDto
                {
                    SupplierId = g.Key,
                    Name = g.First().Supplier.Name.Value,
                    ProductCount = g.Select(p => p.Id).Distinct().Count()
                })
                .OrderByDescending(o => o.ProductCount)
                .ToList();

            var productAttributes = (await _productAttributeRepository.GetProductAttributesAsync(asNoTracking: true, cancellationToken))
                .Where(pa => pa.SubCategoryId == subCategoryGuid)
                .ToList();

            if (productAttributes.Count == 0)
                return Result.Success<ProductFiltersDto, Error>(new ProductFiltersDto { SupplierOptions = supplierOptions });

            var allOptions = await _attributeOptionRepository.GetAttributeOptionsAsync(asNoTracking: true, cancellationToken);
            var optionsByAttributeId = allOptions.ToLookup(o => o.AttributeId);

            // How many distinct products currently carry each option - the
            // "(81)" style counts next to each filter checkbox on the reference site.
            var productCountsByOptionId = products
                .SelectMany(p => p.ProductAttributeValues.Select(pav => (pav.AttributeOptionId, p.Id)))
                .GroupBy(x => x.AttributeOptionId)
                .ToDictionary(g => g.Key, g => g.Select(x => x.Id).Distinct().Count());

            var attributeGroups = productAttributes
                .Select(pa => new ProductFilterGroupDto
                {
                    AttributeId = pa.AttributeId,
                    AttributeName = pa.Attribute.Name.Value,
                    ValueType = pa.Attribute.ValueType.ToString(),
                    // Options nobody has selected yet are dropped - a filter
                    // checkbox that can only ever return zero results isn't useful.
                    Options = optionsByAttributeId[pa.AttributeId]
                        .Select(o => new ProductFilterOptionDto
                        {
                            AttributeOptionId = o.Id,
                            Value = o.Value.Value,
                            ProductCount = productCountsByOptionId.GetValueOrDefault(o.Id)
                        })
                        .Where(o => o.ProductCount > 0)
                        .OrderByDescending(o => o.ProductCount)
                        .ToList()
                })
                .Where(g => g.Options.Count > 0)
                .ToList();

            return Result.Success<ProductFiltersDto, Error>(new ProductFiltersDto { AttributeGroups = attributeGroups, SupplierOptions = supplierOptions });
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
