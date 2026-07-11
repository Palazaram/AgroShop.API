using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<Result<IEnumerable<Supplier>, Error>> GetSuppliersAsync(bool asNoTracking = false, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null, CancellationToken cancellationToken = default);
        Task<Result<Supplier?, Error>> GetSupplierByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
    }
}
