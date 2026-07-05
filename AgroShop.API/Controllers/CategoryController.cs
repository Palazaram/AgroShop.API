using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    public class CategoryController : ApplicationController
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<AddCategoryDto> _addCategoryDtoValidator;
        private readonly IValidator<UpdateCategoryDto> _updateCategoryDtoValidator;

        public CategoryController(
            ICategoryService categoryService,
            IValidator<AddCategoryDto> addCategoryDtoValidator,
            IValidator<UpdateCategoryDto> updateCategoryDtoValidator)
        {
            _categoryService = categoryService;
            _addCategoryDtoValidator = addCategoryDtoValidator;
            _updateCategoryDtoValidator = updateCategoryDtoValidator;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto addCategoryDto, CancellationToken cancellationToken)
        {
            var validationResult = await _addCategoryDtoValidator.ValidateAsync(addCategoryDto, cancellationToken);

            if (!validationResult.IsValid)
                return FromValidation(validationResult);

            var result = await _categoryService.AddAsync(addCategoryDto, cancellationToken);

            return FromResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken)
        {
            var validationResult = await _updateCategoryDtoValidator.ValidateAsync(updateCategoryDto, cancellationToken);

            if (!validationResult.IsValid)
                return FromValidation(validationResult);

            var result = await _categoryService.UpdateAsync(id, updateCategoryDto, cancellationToken);

            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.DeleteAsync(id, cancellationToken);
            return FromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetCategoriesAsync(cancellationToken);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id, cancellationToken);
            return FromResult(result);
        }
    }
}
