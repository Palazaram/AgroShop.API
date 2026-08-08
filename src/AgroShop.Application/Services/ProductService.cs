using AgroShop.Application.Dto.Common;
using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Entities;
using AgroShop.Core.Enums;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

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
            IEnumerable<string>? packages = null,
            string? search = null,
            int page = 1,
            int pageSize = DefaultPageSize,
            ProductSortBy sortBy = ProductSortBy.PriceDesc,
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
            var packageSelections = ParsePackageKeys(packages);
            var searchTerms = ParseSearchQuery(search);
            var isFiltered = subCategoryIdSet.Count > 0
                || optionIds.Count > 0
                || supplierIdSet.Count > 0
                || packageSelections.Count > 0
                || searchTerms != null;

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

            // OR between selected packaging sizes, same semantics again.
            productsQuery = ApplyPackageFilter(productsQuery, packageSelections);

            productsQuery = ApplySearchFilter(productsQuery, searchTerms);

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

        // Packaging is a single filter dimension backed by two columns, so it
        // travels as one composite "amount:unit" key - 5 г and 5 кг are
        // different options, and filtering the two columns independently would
        // conflate them. Invariant culture on both sides so the decimal
        // separator can't shift with the server's locale.
        private static string BuildPackageKey(decimal amount, PackageUnit unit) =>
            $"{amount.ToString(CultureInfo.InvariantCulture)}:{unit}";

        // Anything unparseable is skipped rather than failing the request -
        // these arrive straight from a URL, where a stale or hand-edited key
        // should degrade to "filter not applied", not to an error page.
        private static List<(decimal Amount, PackageUnit Unit)> ParsePackageKeys(IEnumerable<string>? keys)
        {
            var parsed = new List<(decimal, PackageUnit)>();
            if (keys == null)
                return parsed;

            foreach (var key in keys.Distinct())
            {
                var separator = key?.LastIndexOf(':') ?? -1;
                if (key == null || separator <= 0)
                    continue;

                if (decimal.TryParse(key[..separator], NumberStyles.Number, CultureInfo.InvariantCulture, out var amount)
                    && Enum.TryParse<PackageUnit>(key[(separator + 1)..], out var unit))
                    parsed.Add((amount, unit));
            }

            return parsed;
        }

        // EF can't translate Contains over a collection of tuples, and two
        // independent Contains calls (one per column) would match the cross
        // product instead - with 5 г and 1 кг selected, 5 кг would wrongly
        // pass too. Built as an expression tree so it lands as a single
        // "(amount = x AND unit = y) OR (...)" WHERE clause, which still
        // composes with the grouping and paging applied after it.
        private static IQueryable<Product> ApplyPackageFilter(
            IQueryable<Product> query,
            List<(decimal Amount, PackageUnit Unit)> packages)
        {
            if (packages.Count == 0)
                return query;

            var product = Expression.Parameter(typeof(Product), "p");
            var packageSize = Expression.Property(product, nameof(Product.PackageSize));
            var amount = Expression.Property(packageSize, nameof(PackageSize.Amount));
            var unit = Expression.Property(packageSize, nameof(PackageSize.Unit));

            Expression? predicate = null;
            foreach (var (packageAmount, packageUnit) in packages)
            {
                var matches = Expression.AndAlso(
                    Expression.Equal(amount, Expression.Constant(packageAmount)),
                    Expression.Equal(unit, Expression.Constant(packageUnit)));

                predicate = predicate == null ? matches : Expression.OrElse(predicate, matches);
            }

            return query.Where(Expression.Lambda<Func<Product, bool>>(predicate!, product));
        }

        // Trimmed queries under 2 characters aren't an error - they arrive
        // straight from a URL, and "too short to mean anything" should read
        // as "no search", exactly like the unparseable package keys above.
        private static (string[] Words, string Whole)? ParseSearchQuery(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return null;

            var whole = search.Trim();
            if (whole.Length < 2)
                return null;

            return (whole.Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries), whole);
        }

        // %, _ and \ are LIKE metacharacters - a shopper typing "5%" means a
        // literal percent sign, not "5 followed by anything".
        private static string EscapeLikePattern(string value) =>
            value.Replace("\\", "\\\\").Replace("%", "\\%").Replace("_", "\\_");

        private static readonly MethodInfo ILikeWithEscape = typeof(NpgsqlDbFunctionsExtensions).GetMethod(
            nameof(NpgsqlDbFunctionsExtensions.ILike),
            [typeof(DbFunctions), typeof(string), typeof(string), typeof(string)])!;

        // A product matches when every word appears in its name (any order)
        // OR the whole query is a substring of its SKU - words are how people
        // search names, but a SKU is a single token they paste verbatim.
        //
        // Built as an expression tree for the same reason as
        // ApplyPackageFilter: the word list is dynamic, and the AND-chain has
        // to sit inside an OR with the SKU match - chained .Where calls can
        // only express top-level ANDs, and .All() over a captured list is the
        // shape that risks silent client-side evaluation. ILIKE rather than
        // ToLower().Contains() so case-folding (Cyrillic included) happens in
        // Postgres.
        private static IQueryable<Product> ApplySearchFilter(
            IQueryable<Product> query,
            (string[] Words, string Whole)? searchTerms)
        {
            if (searchTerms == null)
                return query;

            var (words, whole) = searchTerms.Value;

            var product = Expression.Parameter(typeof(Product), "p");
            var efFunctions = Expression.Property(null, typeof(EF), nameof(EF.Functions));
            var name = Expression.Property(Expression.Property(product, nameof(Product.Name)), nameof(ProductName.Value));
            var sku = Expression.Property(Expression.Property(product, nameof(Product.Sku)), nameof(Sku.Value));

            Expression Matches(Expression column, string term) => Expression.Call(
                ILikeWithEscape,
                efFunctions,
                column,
                Expression.Constant($"%{EscapeLikePattern(term)}%"),
                Expression.Constant("\\"));

            var nameMatchesEveryWord = words
                .Select(word => Matches(name, word))
                .Aggregate(Expression.AndAlso);

            var predicate = Expression.OrElse(nameMatchesEveryWord, Matches(sku, whole));
            return query.Where(Expression.Lambda<Func<Product, bool>>(predicate, product));
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
            IEnumerable<string>? packages = null,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subCategoryIdSet = subCategoryIds?.Distinct().ToHashSet() ?? [];
            var optionIds = attributeOptionIds?.Distinct().ToList() ?? [];
            var supplierIdSet = supplierIds?.Distinct().ToHashSet() ?? [];
            var packageSelections = ParsePackageKeys(packages);

            var allOptions = await _attributeOptionRepository.GetAttributeOptionsAsync(asNoTracking: true, cancellationToken);
            var optionsById = allOptions.ToDictionary(o => o.Id);

            // Same OR-within-attribute/AND-across-attributes grouping as GetProductsAsync.
            var selectedGroupsByAttributeId = optionIds
                .Where(optionsById.ContainsKey)
                .GroupBy(id => optionsById[id].AttributeId)
                .ToDictionary(g => g.Key, g => g.ToHashSet());

            var baseQuery = _productRepository.GetProductsQueryable(asNoTracking: true);

            IQueryable<Product> BuildQuery(bool includeSubCategory, bool includeSupplier, bool includePackage, Guid? excludeAttributeId)
            {
                var query = baseQuery;

                if (includeSubCategory && subCategoryIdSet.Count > 0)
                    query = query.Where(p => subCategoryIdSet.Contains(p.SubCategoryId));

                if (includeSupplier && supplierIdSet.Count > 0)
                    query = query.Where(p => supplierIdSet.Contains(p.SupplierId));

                if (includePackage)
                    query = ApplyPackageFilter(query, packageSelections);

                foreach (var (attributeId, group) in selectedGroupsByAttributeId)
                {
                    if (attributeId == excludeAttributeId)
                        continue;

                    query = query.Where(p => p.ProductAttributeValues.Any(pav => group.Contains(pav.AttributeOptionId)));
                }

                return query;
            }

            // The "universe" of each facet: everything that exists inside the
            // current subcategory scope, with none of the sibling facet
            // selections applied. Options are reported from this list with a
            // count of 0 when the active selections rule them out, instead of
            // dropping out of the response entirely - that lets the sidebar
            // grey them out in place ("supplier A simply has no 2 кг pack")
            // rather than reshuffling the list under the user's cursor every
            // time a neighbouring facet changes.
            var scopeQuery = subCategoryIdSet.Count > 0
                ? baseQuery.Where(p => subCategoryIdSet.Contains(p.SubCategoryId))
                : baseQuery;

            // Every filter applied, nothing excluded - this is the plain
            // "how many match right now" number, not a facet.
            var totalCount = await BuildQuery(includeSubCategory: true, includeSupplier: true, includePackage: true, excludeAttributeId: null)
                .Select(p => p.Id)
                .Distinct()
                .CountAsync(cancellationToken);

            // SubCategory facet - ignores subCategoryIds itself, keeps supplier + package + attributes.
            var subCategoryOptions = await BuildQuery(includeSubCategory: false, includeSupplier: true, includePackage: true, excludeAttributeId: null)
                .GroupBy(p => p.SubCategoryId)
                .Select(g => new ProductFilterSubCategoryOptionDto
                {
                    SubCategoryId = g.Key,
                    ProductCount = g.Select(p => p.Id).Distinct().Count(),
                })
                .ToListAsync(cancellationToken);

            // Supplier facet - ignores supplierIds itself, keeps subCategory + package + attributes.
            var supplierCounts = await BuildQuery(includeSubCategory: true, includeSupplier: false, includePackage: true, excludeAttributeId: null)
                .GroupBy(p => p.SupplierId)
                .Select(g => new { SupplierId = g.Key, Count = g.Select(p => p.Id).Distinct().Count() })
                .ToListAsync(cancellationToken);

            var supplierUniverse = await scopeQuery
                .Select(p => new { p.SupplierId, Name = p.Supplier.Name.Value })
                .Distinct()
                .ToListAsync(cancellationToken);

            var supplierCountById = supplierCounts.ToDictionary(c => c.SupplierId, c => c.Count);

            // Ordered by name alone, deliberately not by count: counts change
            // whenever a neighbouring facet is ticked, and ordering by them
            // would reshuffle this whole list underneath the user every time.
            // A stable, predictable order is worth more here than surfacing
            // the most populated supplier first.
            var supplierOptions = supplierUniverse
                .Select(s => new ProductFilterSupplierOptionDto
                {
                    SupplierId = s.SupplierId,
                    Name = s.Name,
                    ProductCount = supplierCountById.GetValueOrDefault(s.SupplierId),
                })
                .OrderBy(o => o.Name)
                .ToList();

            // Package facet - ignores packages itself, keeps subCategory +
            // supplier + attributes. Ordered by unit then ascending amount
            // (1 г, 5 г, 10 г, 1 кг, ...) rather than by count: sizes are a
            // scale the user reads along, so the natural order beats
            // popularity here, unlike the supplier list above.
            var packageCounts = await BuildQuery(includeSubCategory: true, includeSupplier: true, includePackage: false, excludeAttributeId: null)
                .GroupBy(p => new { p.PackageSize.Amount, p.PackageSize.Unit })
                .Select(g => new
                {
                    g.Key.Amount,
                    g.Key.Unit,
                    Count = g.Select(p => p.Id).Distinct().Count(),
                })
                .ToListAsync(cancellationToken);

            var packageUniverse = await scopeQuery
                .Select(p => new { p.PackageSize.Amount, p.PackageSize.Unit })
                .Distinct()
                .ToListAsync(cancellationToken);

            var packageCountByKey = packageCounts.ToDictionary(c => BuildPackageKey(c.Amount, c.Unit), c => c.Count);

            var packageOptions = packageUniverse
                .OrderBy(p => p.Unit)
                .ThenBy(p => p.Amount)
                .Select(p => new ProductFilterPackageOptionDto
                {
                    Key = BuildPackageKey(p.Amount, p.Unit),
                    PackageAmount = p.Amount,
                    PackageUnit = p.Unit.ToString(),
                    ProductCount = packageCountByKey.GetValueOrDefault(BuildPackageKey(p.Amount, p.Unit)),
                })
                .ToList();

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
                var scopedQuery = BuildQuery(includeSubCategory: true, includeSupplier: true, includePackage: true, excludeAttributeId: attribute.Id);

                var optionCounts = await scopedQuery
                    .SelectMany(p => p.ProductAttributeValues
                        .Where(pav => pav.AttributeOption.AttributeId == attribute.Id)
                        .Select(pav => new { pav.AttributeOptionId, ProductId = p.Id }))
                    .GroupBy(x => x.AttributeOptionId)
                    .Select(g => new { AttributeOptionId = g.Key, Count = g.Select(x => x.ProductId).Distinct().Count() })
                    .ToListAsync(cancellationToken);

                // Universe for this attribute: every option actually carried by
                // some product in scope, so options ruled out by a neighbouring
                // facet report 0 instead of disappearing. An attribute whose
                // options are on nothing in scope is skipped entirely - that's
                // an attribute wired to the subcategory but never filled in,
                // not a filter with nothing currently matching.
                var optionUniverse = await scopeQuery
                    .SelectMany(p => p.ProductAttributeValues
                        .Where(pav => pav.AttributeOption.AttributeId == attribute.Id)
                        .Select(pav => pav.AttributeOptionId))
                    .Distinct()
                    .ToListAsync(cancellationToken);

                if (optionUniverse.Count == 0)
                    continue;

                var optionCountById = optionCounts.ToDictionary(oc => oc.AttributeOptionId, oc => oc.Count);

                // By value, not by count - same reasoning as the supplier list.
                var options = optionUniverse
                    .Where(optionsById.ContainsKey)
                    .Select(optionId => new ProductFilterOptionDto
                    {
                        AttributeOptionId = optionId,
                        Value = optionsById[optionId].Value.Value,
                        ProductCount = optionCountById.GetValueOrDefault(optionId),
                    })
                    .OrderBy(o => o.Value)
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
                TotalCount = totalCount,
                SubCategoryOptions = subCategoryOptions,
                SupplierOptions = supplierOptions,
                PackageOptions = packageOptions,
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
