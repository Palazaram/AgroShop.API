using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AgroShop.Persistence.Repositories
{
    public class AttributeOptionRepository : IAttributeOptionRepository
    {
        private readonly AgroShopDbContext _context;

        public AttributeOptionRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AttributeOption>> GetAttributeOptionsAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var optionsQuery = _context.AttributeOptions.IncludeAll();

            if (asNoTracking)
            {
                optionsQuery = optionsQuery.AsNoTracking();
            }

            return await optionsQuery.ToListAsync(cancellationToken);
        }

        public async Task<AttributeOption?> GetAttributeOptionByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var optionsQuery = _context.AttributeOptions.IncludeAll();

            if (asNoTracking)
            {
                optionsQuery = optionsQuery.AsNoTracking();
            }

            return await optionsQuery.SingleOrDefaultAsync(o => o.Id == id, cancellationToken);
        }

        public async Task AddAsync(AttributeOption attributeOption, CancellationToken cancellationToken)
        {
            await _context.AttributeOptions.AddAsync(attributeOption, cancellationToken);
        }

        public void Delete(AttributeOption attributeOption)
        {
            _context.Entry(attributeOption).State = EntityState.Deleted;
        }
    }
}
