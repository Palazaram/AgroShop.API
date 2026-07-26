using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AgroShop.Persistence.Repositories
{
    public class ProductAttributeValueRepository : IProductAttributeValueRepository
    {
        private readonly AgroShopDbContext _context;

        public ProductAttributeValueRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductAttributeValue>> GetByProductIdAsync(Guid productId, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var valuesQuery = _context.ProductAttributeValues.IncludeAll().Where(pav => pav.ProductId == productId);

            if (asNoTracking)
            {
                valuesQuery = valuesQuery.AsNoTracking();
            }

            return await valuesQuery.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(ProductAttributeValue value, CancellationToken cancellationToken)
        {
            await _context.ProductAttributeValues.AddAsync(value, cancellationToken);
        }

        public void Delete(ProductAttributeValue value)
        {
            _context.Entry(value).State = EntityState.Deleted;
        }
    }
}
