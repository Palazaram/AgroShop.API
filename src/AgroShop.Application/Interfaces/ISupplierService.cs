using AgroShop.Application.Dto.SupplierDto;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<Result<IEnumerable<SupplierDto>, Error>> GetSuppliersAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<Result<SupplierDto, Error>> GetSupplierByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddSupplierDto supplierDto, CancellationToken cancellationToken);
        Task<Result<SupplierDto, Error>> UpdateAsync(string id, UpdateSupplierDto supplierDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
