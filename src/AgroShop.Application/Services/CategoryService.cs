using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private const string ImagesSubfolder = "categories";

        // Backstop only - Add/Update/Delete below invalidate this explicitly, so the
        // TTL just bounds how stale the cache can get if an invalidation is ever missed.
        private const string CategoriesCacheKey = "categories:all";
        private static readonly TimeSpan CategoriesCacheDuration = TimeSpan.FromMinutes(15);

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _imageStorageService;
        private readonly ICacheService _cacheService;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IImageStorageService imageStorageService,
            ICacheService cacheService)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<CategoryDto>, Error>> GetCategoriesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cached = await _cacheService.GetAsync<List<CategoryDto>>(CategoriesCacheKey, cancellationToken);
            if (cached != null)
                return Result.Success<IEnumerable<CategoryDto>, Error>(cached);

            var categories = await _categoryRepository.GetCategoriesAsync(asNoTracking, cancellationToken);
            var categoryDtos = categories.ToDto().OrderBy(c => c.Name).ToList();

            await _cacheService.SetAsync(CategoriesCacheKey, categoryDtos, CategoriesCacheDuration, cancellationToken);

            return Result.Success<IEnumerable<CategoryDto>, Error>(categoryDtos);
        }

        public async Task<Result<CategoryDto, Error>> GetCategoryByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var categoryId))
                return Result.Failure<CategoryDto, Error>(Errors.General.IncorrectGuidError());

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, asNoTracking, cancellationToken);

            if (category == null)
                return Result.Failure<CategoryDto, Error>(Errors.Category.CategoryIsNullById());

            return Result.Success<CategoryDto, Error>(category.ToDto());
        }

        public async Task<UnitResult<Error>> AddAsync(AddCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var imagePath = await _imageStorageService.SaveAsync(categoryDto.Image, ImagesSubfolder, cancellationToken);

            var categoryResult = Category.Create(categoryDto.Name, imagePath);
            if (categoryResult.IsFailure)
                return UnitResult.Failure(categoryResult.Error);

            await _categoryRepository.AddAsync(categoryResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(CategoriesCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        public async Task<Result<CategoryDto, Error>> UpdateAsync(string id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var categoryId))
                return Result.Failure<CategoryDto, Error>(Errors.General.IncorrectGuidError());

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken: cancellationToken);

            if (category == null)
                return Result.Failure<CategoryDto, Error>(Errors.Category.CategoryIsNullById());

            var previousImagePath = category.ImagePath;

            string? imagePath = null;
            if (categoryDto.Image != null)
                imagePath = await _imageStorageService.SaveAsync(categoryDto.Image, ImagesSubfolder, cancellationToken);

            var updateResult = category.Update(categoryDto.Name, imagePath);
            if (updateResult.IsFailure)
                return Result.Failure<CategoryDto, Error>(updateResult.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(CategoriesCacheKey, cancellationToken);

            // Only drop the old file once the new one is safely persisted.
            if (imagePath != null)
                await _imageStorageService.DeleteAsync(previousImagePath, cancellationToken);

            return Result.Success<CategoryDto, Error>(category.ToDto());
        }

        public async Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var categoryId))
                return UnitResult.Failure(Errors.General.IncorrectGuidError());

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken: cancellationToken);

            if (category == null)
                return UnitResult.Failure(Errors.Category.CategoryIsNullById());

            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(CategoriesCacheKey, cancellationToken);
            await _imageStorageService.DeleteAsync(category.ImagePath, cancellationToken);
            return UnitResult.Success<Error>();
        }
    }
}
