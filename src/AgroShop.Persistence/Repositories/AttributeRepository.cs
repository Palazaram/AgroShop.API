using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using Attribute = AgroShop.Core.Entities.Attribute;

namespace AgroShop.Persistence.Repositories
{
    public class AttributeRepository : IAttributeRepository
    {
        private readonly AgroShopDbContext _context;

        public AttributeRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attribute>> GetAttributesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var attributesQuery = _context.Attributes.IncludeAll();

            if (asNoTracking)
            {
                attributesQuery = attributesQuery.AsNoTracking();
            }

            return await attributesQuery.ToListAsync(cancellationToken);
        }

        public async Task<Attribute?> GetAttributeByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var attributesQuery = _context.Attributes.IncludeAll();

            if (asNoTracking)
            {
                attributesQuery = attributesQuery.AsNoTracking();
            }

            return await attributesQuery.SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
        }

        public async Task AddAsync(Attribute attribute, CancellationToken cancellationToken)
        {
            await _context.Attributes.AddAsync(attribute, cancellationToken);
        }

        public void Delete(Attribute attribute)
        {
            _context.Entry(attribute).State = EntityState.Deleted;
        }
    }
}
