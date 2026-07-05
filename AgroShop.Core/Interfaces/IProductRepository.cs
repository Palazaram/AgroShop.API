using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
using System.Linq.Expressions;

namespace AgroShop.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<Product>, IQueryable<Product>>? filter = null);
        Task<IEnumerable<TDto>> GetProductsDTOAsync<TDto>(Expression<Func<Product, TDto>> selector, CancellationToken cancellationToken, Func<IQueryable<Product>, IQueryable<Product>>? filter = null);
        Task<Product?> GetProductByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = false);
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task DeleteProductAsync(Product product, CancellationToken cancellationToken);
    }
}
