using AgroShop.Application.Dto.Common;
using AgroShop.Application.Dto.ProductDto;
using AgroShop.Core.Enums;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IProductService
    {
        // subCategoryIds/attributeOptionIds/supplierIds left unset returns
        // the full, cached list - passing any switches to an uncached,
        // filtered query (see ProductService for why filtered results
        // aren't cached). subCategoryIds is OR'd like the other two - a
        // category maps to several subcategories, so "all products in this
        // category" means passing all of its subcategory ids at once.
        // page/pageSize are clamped inside the service regardless of what's
        // passed in, so callers don't need to validate them first.
        // packages holds opaque "amount:unit" keys as handed out by
        // GetProductFacetsAsync's PackageOptions - callers pass them straight
        // back rather than building them, and anything unparseable is ignored.
        //
        // search matches every whitespace-separated word against the product
        // name (any order) OR the whole query against the SKU, both
        // case-insensitively; trimmed queries under 2 characters degrade to
        // "no search", never to an error.
        Task<Result<PagedResult<ProductDto>, Error>> GetProductsAsync(
            bool asNoTracking = false,
            IEnumerable<Guid>? subCategoryIds = null,
            IEnumerable<Guid>? attributeOptionIds = null,
            IEnumerable<Guid>? supplierIds = null,
            IEnumerable<string>? packages = null,
            string? search = null,
            int page = 1,
            int pageSize = 20,
            ProductSortBy sortBy = ProductSortBy.PriceDesc,
            CancellationToken cancellationToken = default);
        Task<Result<ProductDto, Error>> GetProductByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddProductDto productDto, CancellationToken cancellationToken);
        Task<Result<ProductDto, Error>> UpdateAsync(string id, UpdateProductDto productDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);

        // Every facet in the returned DTO is computed with the current
        // subCategoryIds/attributeOptionIds/supplierIds applied - except
        // its own dimension, which is deliberately ignored so its own
        // counts reflect "if I additionally picked this" rather than
        // freezing at whatever was true before anything was selected.
        //
        // search is a SCOPE, not a facet: it applies to every dimension, the
        // option universe and TotalCount alike, and is never self-excluded -
        // options with no match inside the search disappear from the
        // response entirely. Same matching semantics as GetProductsAsync.
        Task<Result<ProductFacetsDto, Error>> GetProductFacetsAsync(
            IEnumerable<Guid>? subCategoryIds = null,
            IEnumerable<Guid>? attributeOptionIds = null,
            IEnumerable<Guid>? supplierIds = null,
            IEnumerable<string>? packages = null,
            string? search = null,
            CancellationToken cancellationToken = default);
    }
}
