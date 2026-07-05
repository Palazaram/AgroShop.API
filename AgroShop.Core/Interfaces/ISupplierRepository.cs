using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
using System.Linq.Expressions;

namespace AgroShop.Core.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null);
        Task<IEnumerable<TDto>> GetSuppliersDTOAsync<TDto>(Expression<Func<Supplier, TDto>> selector, CancellationToken cancellationToken, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null);
        Task<Supplier?> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = false);
        Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken);
        Task UpdateSupplierAsync(Supplier supplier, CancellationToken cancellationToken);
        Task DeleteSupplierAsync(Supplier supplier, CancellationToken cancellationToken);
    }
}
