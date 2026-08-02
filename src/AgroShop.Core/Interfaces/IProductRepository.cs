using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface IProductRepository
    {
        IQueryable<Product> GetProductsQueryable(bool asNoTracking = false);
        Task<Product?> GetProductByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddAsync(Product product, CancellationToken cancellationToken);
        void Delete(Product product);
    }
}
