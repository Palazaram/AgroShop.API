using AgroShop.Application.Dto.AttributeOptionDto;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface IAttributeOptionService
    {
        Task<Result<IEnumerable<AttributeOptionDto>, Error>> GetAttributeOptionsAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<Result<AttributeOptionDto, Error>> GetAttributeOptionByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddAttributeOptionDto attributeOptionDto, CancellationToken cancellationToken);
        Task<Result<AttributeOptionDto, Error>> UpdateAsync(string id, UpdateAttributeOptionDto attributeOptionDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
