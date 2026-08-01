using AgroShop.Application.Dto.ProductDto;
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
        Task<Result<IEnumerable<ProductDto>, Error>> GetProductsAsync(
            bool asNoTracking = false,
            IEnumerable<Guid>? subCategoryIds = null,
            IEnumerable<Guid>? attributeOptionIds = null,
            IEnumerable<Guid>? supplierIds = null,
            CancellationToken cancellationToken = default);
        Task<Result<ProductDto, Error>> GetProductByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddProductDto productDto, CancellationToken cancellationToken);
        Task<Result<ProductDto, Error>> UpdateAsync(string id, UpdateProductDto productDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
        Task<Result<ProductFiltersDto, Error>> GetFiltersBySubCategoryAsync(string subCategoryId, CancellationToken cancellationToken = default);
    }
}
