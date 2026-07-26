using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliersAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<Supplier?> GetSupplierByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddAsync(Supplier supplier, CancellationToken cancellationToken);
        void Delete(Supplier supplier);
    }
}
