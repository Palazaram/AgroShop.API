using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Application.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(
            ICategoryRepository categoryRepository,
            IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IEnumerable<Category>, Error>> GetCategoriesAsync(bool asNoTracking = false, Func<IQueryable<Category>, IQueryable<Category>>? filter = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var categories = await _categoryRepository.GetCategoriesAsync(asNoTracking, filter, cancellationToken);
            return Result.Success<IEnumerable<Category>, Error>(categories);
        }

        public async Task<Result<Category, Error>> GetCategoryByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var categoryId))
                return Result.Failure<Category, Error>(Errors.General.IncorrectGuidError());

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, asNoTracking, cancellationToken);

            if (category == null)
                return Result.Failure<Category, Error>(Errors.Category.CategoryIsNullById());

            return Result.Success<Category, Error>(category);
        }

        public async Task<UnitResult<Error>> AddAsync(AddCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var categoryResult = Category.Create(categoryDto.Name);
            if (categoryResult.IsFailure)
                return UnitResult.Failure(categoryResult.Error);

            await _categoryRepository.AddAsync(categoryResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return UnitResult.Success<Error>();
        }

        public async Task<Result<Category, Error>> UpdateAsync(string id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var categoryId))
                return Result.Failure<Category, Error>(Errors.General.IncorrectGuidError());

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken: cancellationToken);

            if (category == null)
                return Result.Failure<Category, Error>(Errors.Category.CategoryIsNullById());

            var updateResult = category.Update(categoryDto.Name);
            if (updateResult.IsFailure)
                return Result.Failure<Category, Error>(updateResult.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success<Category, Error>(category);
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
            return UnitResult.Success<Error>();
        }
    }
}
