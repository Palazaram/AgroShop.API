using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    public class CategoryController : ApplicationController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryDto addCategoryDto, CancellationToken cancellationToken)
        {
            var result = await _categoryService.AddAsync(addCategoryDto, cancellationToken);

            return FromResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, UpdateCategoryDto updateCategoryDto, CancellationToken cancellationToken)
        {
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
            var result = await _categoryService.GetCategoriesAsync(cancellationToken: cancellationToken);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(string id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetCategoryByIdAsync(id, cancellationToken: cancellationToken);
            return FromResult(result);
        }
    }
}
