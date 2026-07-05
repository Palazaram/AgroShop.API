using AgroShop.Application.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly ISubCategoryRepository _subCategoryRepository;

        public SubCategoryService(ISubCategoryRepository subCategoryRepository)
        {
            _subCategoryRepository = subCategoryRepository;
        }

        public async Task<Result<IEnumerable<SubCategory>, Error>> GetSubCategoriesAsync(bool asNoTracking = false, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var subCategories = await _subCategoryRepository.GetSubCategoriesAsync(asNoTracking, filter, cancellationToken);
            return Result.Success<IEnumerable<SubCategory>, Error>(subCategories);
        }

        public async Task<Result<SubCategory?, Error>> GetSubCategoryByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(id, asNoTracking, cancellationToken);
            return Result.Success<SubCategory?, Error>(subCategory);
        }
    }
}
