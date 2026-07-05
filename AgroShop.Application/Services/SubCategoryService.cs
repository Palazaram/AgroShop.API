using AgroShop.Application.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
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

        public async Task<Result<IEnumerable<SubCategory>, Error>> GetSubCategoriesAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<SubCategory>, IQueryable<SubCategory>>? filter = null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var subCategories = await _subCategoryRepository.GetSubCategoriesAsync(cancellationToken, asNoTracking, filter);
            return Result.Success<IEnumerable<SubCategory>, Error>(subCategories);
        }

        public async Task<Result<SubCategory?, Error>> GetSubCategoryByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(id, cancellationToken, asNoTracking);
            return Result.Success<SubCategory?, Error>(subCategory);
        }
    }
}
