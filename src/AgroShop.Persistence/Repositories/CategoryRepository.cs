using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using AgroShop.Persistence.Extensions;

namespace AgroShop.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AgroShopDbContext _context;

        public CategoryRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync(bool asNoTracking = false, Func<IQueryable<Category>, IQueryable<Category>>? filter = null, CancellationToken cancellationToken = default)
        {
            var categoriesQuery = _context.Categories.IncludeAll();

            if (asNoTracking)
            {
                categoriesQuery = categoriesQuery.AsNoTracking();
            }

            if (filter != null)
            {
                categoriesQuery = filter(categoriesQuery);
            }

            return await categoriesQuery.ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var categoriesQuery = _context.Categories.IncludeAll();

            if (asNoTracking)
            {
                categoriesQuery = categoriesQuery.AsNoTracking();
            }

            return await categoriesQuery.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken)
        {
            await _context.Categories.AddAsync(category, cancellationToken);
        }

        public void Delete(Category category)
        {
            _context.Entry(category).State = EntityState.Deleted;
        }
    }
}
