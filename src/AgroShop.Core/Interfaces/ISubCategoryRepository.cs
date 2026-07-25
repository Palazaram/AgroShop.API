using AgroShop.Core.Entities;

namespace AgroShop.Core.Interfaces
{
    public interface ISubCategoryRepository
    {
        Task<IEnumerable<SubCategory>> GetSubCategoriesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<SubCategory?> GetSubCategoryByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task AddAsync(SubCategory subCategory, CancellationToken cancellationToken);
        void Delete(SubCategory subCategory);
    }
}
