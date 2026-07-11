using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface ISubCategoryService
    {
        Task<Result<IEnumerable<SubCategory>, Error>> GetSubCategoriesAsync(bool asNoTracking = false, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null, CancellationToken cancellationToken = default);
        Task<Result<SubCategory?, Error>> GetSubCategoryByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default);
    }
}
