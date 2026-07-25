using AgroShop.Application.Dto.SubCategoryDto;
using AgroShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    public class SubCategoryController : ApplicationController
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory([FromBody] AddSubCategoryDto addSubCategoryDto, CancellationToken cancellationToken)
        {
            var result = await _subCategoryService.AddAsync(addSubCategoryDto, cancellationToken);
            return FromResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(string id, [FromBody] UpdateSubCategoryDto updateSubCategoryDto, CancellationToken cancellationToken)
        {
            var result = await _subCategoryService.UpdateAsync(id, updateSubCategoryDto, cancellationToken);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(string id, CancellationToken cancellationToken)
        {
            var result = await _subCategoryService.DeleteAsync(id, cancellationToken);
            return FromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSubCategories(CancellationToken cancellationToken)
        {
            var result = await _subCategoryService.GetSubCategoriesAsync(cancellationToken: cancellationToken);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubCategoryById(string id, CancellationToken cancellationToken)
        {
            var result = await _subCategoryService.GetSubCategoryByIdAsync(id, cancellationToken: cancellationToken);
            return FromResult(result);
        }
    }
}
