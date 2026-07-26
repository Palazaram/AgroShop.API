using AgroShop.Application.Dto.AttributeOptionDto;
using AgroShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    public class AttributeOptionController : ApplicationController
    {
        private readonly IAttributeOptionService _attributeOptionService;

        public AttributeOptionController(IAttributeOptionService attributeOptionService)
        {
            _attributeOptionService = attributeOptionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddAttributeOption([FromBody] AddAttributeOptionDto addAttributeOptionDto, CancellationToken cancellationToken)
        {
            var result = await _attributeOptionService.AddAsync(addAttributeOptionDto, cancellationToken);
            return FromResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAttributeOption(string id, [FromBody] UpdateAttributeOptionDto updateAttributeOptionDto, CancellationToken cancellationToken)
        {
            var result = await _attributeOptionService.UpdateAsync(id, updateAttributeOptionDto, cancellationToken);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttributeOption(string id, CancellationToken cancellationToken)
        {
            var result = await _attributeOptionService.DeleteAsync(id, cancellationToken);
            return FromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAttributeOptions(CancellationToken cancellationToken)
        {
            var result = await _attributeOptionService.GetAttributeOptionsAsync(cancellationToken: cancellationToken);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAttributeOptionById(string id, CancellationToken cancellationToken)
        {
            var result = await _attributeOptionService.GetAttributeOptionByIdAsync(id, cancellationToken: cancellationToken);
            return FromResult(result);
        }
    }
}
