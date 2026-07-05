using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
using System.Linq.Expressions;

namespace AgroShop.Core.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategory>> GetSubCategoriesAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null);
        Task<IEnumerable<TDto>> GetSubCategoriesDTOAsync<TDto>(Expression<Func<SubCategory, TDto>> selector, CancellationToken cancellationToken, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null);
        Task<SubCategory?> GetSubCategoryByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = false);
        Task AddSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task UpdateSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
        Task DeleteSubCategoryAsync(SubCategory subCategory, CancellationToken cancellationToken);
    }
}
