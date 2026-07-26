using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<Product?> GetProductByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddAsync(Product product, CancellationToken cancellationToken);
        void Delete(Product product);
    }
}
