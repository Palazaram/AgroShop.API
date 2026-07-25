using AgroShop.Application.Dto.SubCategoryDto;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Interfaces
{
    public interface ISubCategoryService
    {
        Task<Result<IEnumerable<SubCategoryDto>, Error>> GetSubCategoriesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<Result<SubCategoryDto, Error>> GetSubCategoryByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default);
        Task<UnitResult<Error>> AddAsync(AddSubCategoryDto subCategoryDto, CancellationToken cancellationToken);
        Task<Result<SubCategoryDto, Error>> UpdateAsync(string id, UpdateSubCategoryDto subCategoryDto, CancellationToken cancellationToken);
        Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
