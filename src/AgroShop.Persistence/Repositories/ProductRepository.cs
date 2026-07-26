using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AgroShop.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AgroShopDbContext _context;

        public ProductRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var productsQuery = _context.Products.IncludeAll();

            if (asNoTracking)
            {
                productsQuery = productsQuery.AsNoTracking();
            }

            return await productsQuery.ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetProductByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var productQuery = _context.Products.IncludeAll();

            if (asNoTracking)
            {
                productQuery = productQuery.AsNoTracking();
            }

            return await productQuery.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _context.Products.AddAsync(product, cancellationToken);
        }

        public void Delete(Product product)
        {
            _context.Entry(product).State = EntityState.Deleted;
        }
    }
}
