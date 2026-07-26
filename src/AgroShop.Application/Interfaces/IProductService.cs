using AgroShop.Application.Dto.ProductDto;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IProductService
    {
        // subCategoryId/attributeOptionIds left unset returns the full, cached
        // list - passing either switches to an uncached, filtered query (see
        // ProductService for why filtered results aren't cached).
        Task<Result<IEnumerable<ProductDto>, Error>> GetProductsAsync(
            bool asNoTracking = false,
            Guid? subCategoryId = null,
            IEnumerable<Guid>? attributeOptionIds = null,
            CancellationToken cancellationToken = default);
        Task<Result<ProductDto, Error>> GetProductByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddProductDto productDto, CancellationToken cancellationToken);
        Task<Result<ProductDto, Error>> UpdateAsync(string id, UpdateProductDto productDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
        Task<Result<IEnumerable<ProductFilterGroupDto>, Error>> GetFiltersBySubCategoryAsync(string subCategoryId, CancellationToken cancellationToken = default);
    }
}
