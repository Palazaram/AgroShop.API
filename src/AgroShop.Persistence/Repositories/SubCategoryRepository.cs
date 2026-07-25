using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using AgroShop.Persistence.Extensions;

namespace AgroShop.Persistence.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AgroShopDbContext _context;

        public SubCategoryRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoriesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var subCategoriesQuery = _context.SubCategories.IncludeAll();

            if (asNoTracking)
            {
                subCategoriesQuery = subCategoriesQuery.AsNoTracking();
            }

            return await subCategoriesQuery.ToListAsync(cancellationToken);
        }

        public async Task<SubCategory?> GetSubCategoryByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var subCategoriesQuery = _context.SubCategories.IncludeAll();

            if (asNoTracking)
            {
                subCategoriesQuery = subCategoriesQuery.AsNoTracking();
            }

            return await subCategoriesQuery.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task AddAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            await _context.SubCategories.AddAsync(subCategory, cancellationToken);
        }

        public void Delete(SubCategory subCategory)
        {
            _context.Entry(subCategory).State = EntityState.Deleted;
        }
    }
}
