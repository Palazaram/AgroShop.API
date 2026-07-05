using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<Category>, IQueryable<Category>>? filter = null);
        Task<Category?> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = false);
        Task AddAsync(Category category, CancellationToken cancellationToken);
        void Delete(Category category);
        //Task<IEnumerable<TDto>> GetCategoriesDTOAsync<TDto>(Expression<Func<Category, TDto>> selector, CancellationToken cancellationToken, Func<IQueryable<Category>, IQueryable<Category>>? filter = null);
    }
}
