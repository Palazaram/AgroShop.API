using AgroShop.Core.Entities;
using System.Linq.Expressions;

namespace AgroShop.Core.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync(bool asNoTracking = false, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDto>> GetSuppliersDTOAsync<TDto>(Expression<Func<Supplier, TDto>> selector, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null, CancellationToken cancellationToken = default);
        Task<Supplier?> GetSupplierByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken);
        Task UpdateSupplierAsync(Supplier supplier, CancellationToken cancellationToken);
        Task DeleteSupplierAsync(Supplier supplier, CancellationToken cancellationToken);
    }
}
