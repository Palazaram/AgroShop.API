using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface ISubCategoryService
    {
        Task<Result<IEnumerable<SubCategory>, Error>> GetSubCategoriesAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null);
        Task<Result<SubCategory?, Error>> GetSubCategoryByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = false);
    }
}
