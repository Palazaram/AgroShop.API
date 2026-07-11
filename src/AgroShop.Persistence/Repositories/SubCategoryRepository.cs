using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AgroShop.Persistence.Extensions;
using AgroShop.Core.ValueObjects;

namespace AgroShop.Persistence.Repositories
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AgroShopDbContext _context;

        public SubCategoryRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategoriesAsync(bool asNoTracking = false, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subCategoriesQuery = _context.SubCategories.IncludeAll();

            if (asNoTracking)
            {
                subCategoriesQuery = subCategoriesQuery.AsNoTracking();
            }

            if (filter != null)
            {
                subCategoriesQuery = filter(subCategoriesQuery);
            }

            return await subCategoriesQuery.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TDto>> GetSubCategoriesDTOAsync<TDto>(Expression<Func<SubCategory, TDto>> selector, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subCategoriesQuery = _context.SubCategories.AsNoTracking();

            if (filter != null)
            {
                subCategoriesQuery = filter(subCategoriesQuery);
            }

            return await subCategoriesQuery.Select(selector).ToListAsync(cancellationToken);
        }

        public async Task<SubCategory?> GetSubCategoryByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var subCategoriesQuery = _context.SubCategories.IncludeAll();

            if (asNoTracking)
            {
                subCategoriesQuery = subCategoriesQuery.AsNoTracking();
            }

            return await subCategoriesQuery.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        }

        public async Task AddSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.SubCategories.AddAsync(subCategory);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.SubCategories.Update(subCategory);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.Entry(subCategory).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
