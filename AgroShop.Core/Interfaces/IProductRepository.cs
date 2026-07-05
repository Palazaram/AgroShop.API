using AgroShop.Core.Entities;
using System.Linq.Expressions;

namespace AgroShop.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(bool asNoTracking = false, Func<IQueryable<Product>, IQueryable<Product>>? filter = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDto>> GetProductsDTOAsync<TDto>(Expression<Func<Product, TDto>> selector, Func<IQueryable<Product>, IQueryable<Product>>? filter = null, CancellationToken cancellationToken = default);
        Task<Product?> GetProductByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task DeleteProductAsync(Product product, CancellationToken cancellationToken);
    }
}
