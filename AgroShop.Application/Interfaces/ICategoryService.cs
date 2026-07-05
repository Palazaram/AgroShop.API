using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Core.Entities;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<Category>, Error>> GetCategoriesAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<Category>, IQueryable<Category>>? filter = null);
        Task<Result<Category, Error>> GetCategoryByIdAsync(string id, CancellationToken cancellationToken, bool asNoTracking = false);
        Task<UnitResult<Error>> AddAsync(AddCategoryDto categoryDto, CancellationToken cancellationToken);
        Task<Result<Category, Error>> UpdateAsync(string id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
