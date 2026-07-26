using AgroShop.Application.Dto.ProductAttributeDto;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IProductAttributeService
    {
        Task<Result<IEnumerable<ProductAttributeDto>, Error>> GetProductAttributesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<Result<ProductAttributeDto, Error>> GetProductAttributeByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddProductAttributeDto productAttributeDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
