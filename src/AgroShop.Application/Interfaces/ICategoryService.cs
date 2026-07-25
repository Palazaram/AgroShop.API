using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Result<IEnumerable<CategoryDto>, Error>> GetCategoriesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<Result<CategoryDto, Error>> GetCategoryByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddCategoryDto categoryDto, CancellationToken cancellationToken);
        Task<Result<CategoryDto, Error>> UpdateAsync(string id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
