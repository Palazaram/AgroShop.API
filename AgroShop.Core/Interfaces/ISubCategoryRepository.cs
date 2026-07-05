using AgroShop.Core.Entities;
using System.Linq.Expressions;

namespace AgroShop.Core.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategory>> GetSubCategoriesAsync(bool asNoTracking = false, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null, CancellationToken cancellationToken = default);
        Task<IEnumerable<TDto>> GetSubCategoriesDTOAsync<TDto>(Expression<Func<SubCategory, TDto>> selector, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null, CancellationToken cancellationToken = default);
        Task<SubCategory?> GetSubCategoryByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task UpdateSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task DeleteSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
    }
}
