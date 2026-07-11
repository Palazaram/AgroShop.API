using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgroShop.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AgroShopDbContext _context;

        public ProductRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(bool asNoTracking = false, Func<IQueryable<Product>, IQueryable<Product>>? filter = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var productsQuery = _context.Products.IncludeAll();

            if (asNoTracking) 
            {
                productsQuery = productsQuery.AsNoTracking();
            }

            if (filter != null)
            {
                productsQuery = filter(productsQuery);
            }

            return await productsQuery.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TDto>> GetProductsDTOAsync<TDto>(Expression<Func<Product, TDto>> selector, Func<IQueryable<Product>, IQueryable<Product>>? filter = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var productsQuery = _context.Products.AsNoTracking();

            if (filter != null)
            {
                productsQuery = filter(productsQuery);
            }

            return await productsQuery.Select(selector).ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var productQuery = _context.Products.IncludeAll();

            if (asNoTracking)
            {
                productQuery = productQuery.AsNoTracking();
            }
                
            return await productQuery.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteProductAsync(Product product, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.Entry(product).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
