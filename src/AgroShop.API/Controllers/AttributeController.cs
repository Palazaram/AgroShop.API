using AgroShop.Application.Dto.AttributeDto;
using AgroShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    public class AttributeController : ApplicationController
    {
        private readonly IAttributeService _attributeService;

        public AttributeController(IAttributeService attributeService)
        {
            _attributeService = attributeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAttribute([FromBody] AddAttributeDto addAttributeDto, CancellationToken cancellationToken)
        {
            var result = await _attributeService.AddAsync(addAttributeDto, cancellationToken);
            return FromResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttribute(string id, [FromBody] UpdateAttributeDto updateAttributeDto, CancellationToken cancellationToken)
        {
            var result = await _attributeService.UpdateAsync(id, updateAttributeDto, cancellationToken);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttribute(string id, CancellationToken cancellationToken)
        {
            var result = await _attributeService.DeleteAsync(id, cancellationToken);
            return FromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttributes(CancellationToken cancellationToken)
        {
            var result = await _attributeService.GetAttributesAsync(cancellationToken: cancellationToken);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttributeById(string id, CancellationToken cancellationToken)
        {
            var result = await _attributeService.GetAttributeByIdAsync(id, cancellationToken: cancellationToken);
            return FromResult(result);
        }
    }
}
