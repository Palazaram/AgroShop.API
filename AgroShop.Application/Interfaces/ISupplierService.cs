using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface ISupplierService
    {
        Task<Result<IEnumerable<Supplier>, Error>> GetSuppliersAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null);
        Task<Result<Supplier?, Error>> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = false);
    }
}
