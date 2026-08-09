using AgroShop.Application.Dto.SubCategoryDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class SubCategoryService : ISubCategoryService
    {
        // Backstop only - Add/Update/Delete below invalidate this explicitly, same as
        // CategoryService's cache.
        private const string SubCategoriesCacheKey = "subcategories:all";
        private static readonly TimeSpan SubCategoriesCacheDuration = TimeSpan.FromMinutes(15);

        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public SubCategoryService(
            ISubCategoryRepository subCategoryRepository,
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<SubCategoryDto>, Error>> GetSubCategoriesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cached = await _cacheService.GetAsync<List<SubCategoryDto>>(SubCategoriesCacheKey, cancellationToken);
            if (cached != null)
                return Result.Success<IEnumerable<SubCategoryDto>, Error>(cached);

            var subCategories = await _subCategoryRepository.GetSubCategoriesAsync(asNoTracking, cancellationToken);
            var subCategoryDtos = subCategories.ToDto().OrderBy(sc => sc.Name).ToList();

            await _cacheService.SetAsync(SubCategoriesCacheKey, subCategoryDtos, SubCategoriesCacheDuration, cancellationToken);

            return Result.Success<IEnumerable<SubCategoryDto>, Error>(subCategoryDtos);
        }

        public async Task<Result<SubCategoryDto, Error>> GetSubCategoryByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var subCategoryId))
                return Result.Failure<SubCategoryDto, Error>(Errors.General.IncorrectGuidError());

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(subCategoryId, asNoTracking, cancellationToken);

            if (subCategory == null)
                return Result.Failure<SubCategoryDto, Error>(Errors.SubCategory.SubCategoryNotFoundById());

            return Result.Success<SubCategoryDto, Error>(subCategory.ToDto());
        }

        public async Task<UnitResult<Error>> AddAsync(AddSubCategoryDto subCategoryDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var category = await _categoryRepository.GetCategoryByIdAsync(subCategoryDto.CategoryId, cancellationToken: cancellationToken);
            if (category == null)
                return UnitResult.Failure(Errors.Category.CategoryReferenceNotFound());

            if (await NameTakenInCategoryAsync(subCategoryDto.Name, subCategoryDto.CategoryId, excludeId: null, cancellationToken))
                return UnitResult.Failure(Errors.SubCategory.SubCategoryNameAlreadyExistsInCategory());

            var subCategoryResult = SubCategory.Create(subCategoryDto.Name, subCategoryDto.CategoryId);
            if (subCategoryResult.IsFailure)
                return UnitResult.Failure(subCategoryResult.Error);

            await _subCategoryRepository.AddAsync(subCategoryResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(SubCategoriesCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        public async Task<Result<SubCategoryDto, Error>> UpdateAsync(string id, UpdateSubCategoryDto subCategoryDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var subCategoryId))
                return Result.Failure<SubCategoryDto, Error>(Errors.General.IncorrectGuidError());

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(subCategoryId, cancellationToken: cancellationToken);
            if (subCategory == null)
                return Result.Failure<SubCategoryDto, Error>(Errors.SubCategory.SubCategoryNotFoundById());

            var category = await _categoryRepository.GetCategoryByIdAsync(subCategoryDto.CategoryId, cancellationToken: cancellationToken);
            if (category == null)
                return Result.Failure<SubCategoryDto, Error>(Errors.Category.CategoryReferenceNotFound());

            if (await NameTakenInCategoryAsync(subCategoryDto.Name, subCategoryDto.CategoryId, excludeId: subCategoryId, cancellationToken))
                return Result.Failure<SubCategoryDto, Error>(Errors.SubCategory.SubCategoryNameAlreadyExistsInCategory());

            var updateResult = subCategory.Update(subCategoryDto.Name, subCategoryDto.CategoryId);
            if (updateResult.IsFailure)
                return Result.Failure<SubCategoryDto, Error>(updateResult.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(SubCategoriesCacheKey, cancellationToken);

            // Built from `category` (already fetched above) rather than
            // subCategory.ToDto() - Update() nulls the SubCategory.Category nav
            // property when CategoryId changes, so that path would throw here.
            return Result.Success<SubCategoryDto, Error>(new SubCategoryDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name.Value,
                CategoryId = subCategory.CategoryId,
                CategoryName = category.Name.Value
            });
        }

        public async Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var subCategoryId))
                return UnitResult.Failure(Errors.General.IncorrectGuidError());

            var subCategory = await _subCategoryRepository.GetSubCategoryByIdAsync(subCategoryId, cancellationToken: cancellationToken);
            if (subCategory == null)
                return UnitResult.Failure(Errors.SubCategory.SubCategoryNotFoundById());

            _subCategoryRepository.Delete(subCategory);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(SubCategoriesCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        // Enforces per-category name uniqueness at the application level - see the
        // comment in SubCategoryConfiguration for why this isn't a database index.
        private async Task<bool> NameTakenInCategoryAsync(string name, Guid categoryId, Guid? excludeId, CancellationToken cancellationToken)
        {
            var subCategories = await _subCategoryRepository.GetSubCategoriesAsync(asNoTracking: true, cancellationToken);
            var trimmedName = name.Trim();

            return subCategories.Any(sc =>
                sc.Id != excludeId
                && sc.CategoryId == categoryId
                && string.Equals(sc.Name.Value, trimmedName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
