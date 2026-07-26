using AgroShop.Application.Dto.AttributeDto;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IAttributeService
    {
        Task<Result<IEnumerable<AttributeDto>, Error>> GetAttributesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<Result<AttributeDto, Error>> GetAttributeByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddAttributeDto attributeDto, CancellationToken cancellationToken);
        Task<Result<AttributeDto, Error>> UpdateAsync(string id, UpdateAttributeDto attributeDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
