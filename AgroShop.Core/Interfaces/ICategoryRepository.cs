using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(bool asNoTracking = false, Func<IQueryable<Category>, IQueryable<Category>>? filter = null, CancellationToken cancellationToken = default);
        Task<Category?> GetCategoryByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddAsync(Category category, CancellationToken cancellationToken);
        void Delete(Category category);
    }
}
