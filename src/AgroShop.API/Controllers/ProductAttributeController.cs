using AgroShop.Application.Dto.ProductAttributeDto;
using AgroShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    public class ProductAttributeController : ApplicationController
    {
        private readonly IProductAttributeService _productAttributeService;

        public ProductAttributeController(IProductAttributeService productAttributeService)
        {
            _productAttributeService = productAttributeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAttribute([FromBody] AddProductAttributeDto addProductAttributeDto, CancellationToken cancellationToken)
        {
            var result = await _productAttributeService.AddAsync(addProductAttributeDto, cancellationToken);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAttribute(string id, CancellationToken cancellationToken)
        {
            var result = await _productAttributeService.DeleteAsync(id, cancellationToken);
            return FromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductAttributes(CancellationToken cancellationToken)
        {
            var result = await _productAttributeService.GetProductAttributesAsync(cancellationToken: cancellationToken);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAttributeById(string id, CancellationToken cancellationToken)
        {
            var result = await _productAttributeService.GetProductAttributeByIdAsync(id, cancellationToken: cancellationToken);
            return FromResult(result);
        }
    }
}
