using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<Category>, Error>> GetCategoriesAsync(bool asNoTracking = false, Func<IQueryable<Category>, IQueryable<Category>>? filter = null, CancellationToken cancellationToken = default);
        Task<Result<Category, Error>> GetCategoryByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddCategoryDto categoryDto, CancellationToken cancellationToken);
        Task<Result<Category, Error>> UpdateAsync(string id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
