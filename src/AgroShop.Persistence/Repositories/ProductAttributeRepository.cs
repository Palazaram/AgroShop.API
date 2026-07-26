using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AgroShop.Persistence.Repositories
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        private readonly AgroShopDbContext _context;

        public ProductAttributeRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductAttribute>> GetProductAttributesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var productAttributesQuery = _context.ProductAttributes.IncludeAll();

            if (asNoTracking)
            {
                productAttributesQuery = productAttributesQuery.AsNoTracking();
            }

            return await productAttributesQuery.ToListAsync(cancellationToken);
        }

        public async Task<ProductAttribute?> GetProductAttributeByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var productAttributesQuery = _context.ProductAttributes.IncludeAll();

            if (asNoTracking)
            {
                productAttributesQuery = productAttributesQuery.AsNoTracking();
            }

            return await productAttributesQuery.SingleOrDefaultAsync(pa => pa.Id == id, cancellationToken);
        }

        public async Task AddAsync(ProductAttribute productAttribute, CancellationToken cancellationToken)
        {
            await _context.ProductAttributes.AddAsync(productAttribute, cancellationToken);
        }

        public void Delete(ProductAttribute productAttribute)
        {
            _context.Entry(productAttribute).State = EntityState.Deleted;
        }
    }
}
