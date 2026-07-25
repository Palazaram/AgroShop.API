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

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _imageStorageService;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork,
            IImageStorageService imageStorageService)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;
        }

        public async Task<Result<IEnumerable<CategoryDto>, Error>> GetCategoriesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var categories = await _categoryRepository.GetCategoriesAsync(asNoTracking, cancellationToken);
            return Result.Success<IEnumerable<CategoryDto>, Error>(categories.ToDto());
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
            await _imageStorageService.DeleteAsync(category.ImagePath, cancellationToken);
            return UnitResult.Success<Error>();
        }
    }
}
