using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.Interfaces;
using AgroShop.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    public class ProductController : ApplicationController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] AddProductDto addProductDto, CancellationToken cancellationToken)
        {
            var result = await _productService.AddAsync(addProductDto, cancellationToken);
            return FromResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(string id, [FromForm] UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateAsync(id, updateProductDto, cancellationToken);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteAsync(id, cancellationToken);
            return FromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(
            [FromQuery] Guid[]? subCategoryIds,
            [FromQuery] Guid[]? attributeOptionIds,
            [FromQuery] Guid[]? supplierIds,
            [FromQuery] string[]? packages,
            [FromQuery] string? search = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] ProductSortBy sortBy = ProductSortBy.PriceDesc,
            CancellationToken cancellationToken = default)
        {
            var result = await _productService.GetProductsAsync(
                subCategoryIds: subCategoryIds,
                attributeOptionIds: attributeOptionIds,
                supplierIds: supplierIds,
                packages: packages,
                search: search,
                page: page,
                pageSize: pageSize,
                sortBy: sortBy,
                cancellationToken: cancellationToken);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id, CancellationToken cancellationToken)
        {
            var result = await _productService.GetProductByIdAsync(id, cancellationToken: cancellationToken);
            return FromResult(result);
        }

        [HttpGet("facets")]
        public async Task<IActionResult> GetFacets(
            [FromQuery] Guid[]? subCategoryIds,
            [FromQuery] Guid[]? attributeOptionIds,
            [FromQuery] Guid[]? supplierIds,
            [FromQuery] string[]? packages,
            [FromQuery] string? search,
            CancellationToken cancellationToken)
        {
            var result = await _productService.GetProductFacetsAsync(
                subCategoryIds: subCategoryIds,
                attributeOptionIds: attributeOptionIds,
                supplierIds: supplierIds,
                packages: packages,
                search: search,
                cancellationToken: cancellationToken);
            return FromResult(result);
        }
    }
}
