using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Application.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
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

        public async Task<Result<IEnumerable<Category>, Error>> GetCategoriesAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<Category>, IQueryable<Category>>? filter = null)
        {
            var categories = await _categoryRepository.GetCategoriesAsync(cancellationToken, asNoTracking, filter);
            return Result.Success<IEnumerable<Category>, Error>(categories);
        }

        public async Task<Result<Category, Error>> GetCategoryByIdAsync(string id, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            if(!Guid.TryParse(id, out var categoryId))
                return Result.Failure<Category, Error>(Errors.General.IncorrectGuidError());

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken, asNoTracking);

            if (category == null)
                return Result.Failure<Category, Error>(Errors.Category.CategoryIsNullById());

            return Result.Success<Category, Error>(category);
        }

        public async Task<Result<object?, Error>> AddAsync(AddCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            var categoryName = CategoryName.Create(categoryDto.Name).Value;
            var category = Category.Create(categoryName);
            await _categoryRepository.AddAsync(category, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success<object?, Error>(null);
        }

        public async Task<Result<Category, Error>> UpdateAsync(string id, UpdateCategoryDto categoryDto, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var categoryId))
                return Result.Failure<Category, Error>(Errors.General.IncorrectGuidError());

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken, asNoTracking: false);

            if (category == null)
                return Result.Failure<Category, Error>(Errors.Category.CategoryIsNullById());

            var categoryName = CategoryName.Create(categoryDto.Name).Value;
            category.Update(categoryName);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success<Category, Error>(category);
        }

        public async Task<Result<object?, Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            if (!Guid.TryParse(id, out var categoryId))
                return Result.Failure<Category, Error>(Errors.General.IncorrectGuidError());

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId, cancellationToken, asNoTracking: false);

            if (category == null)
                return Result.Failure<object?, Error>(Errors.Category.CategoryIsNullById());

            _categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success<object?, Error>(null);
        }
    }
}
