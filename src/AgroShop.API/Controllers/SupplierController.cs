using AgroShop.Application.Dto.SupplierDto;
using AgroShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AgroShop.API.Controllers
{
    public class SupplierController : ApplicationController
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpPost]
        public async Task<IActionResult> AddSupplier([FromBody] AddSupplierDto addSupplierDto, CancellationToken cancellationToken)
        {
            var result = await _supplierService.AddAsync(addSupplierDto, cancellationToken);
            return FromResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSupplier(string id, [FromBody] UpdateSupplierDto updateSupplierDto, CancellationToken cancellationToken)
        {
            var result = await _supplierService.UpdateAsync(id, updateSupplierDto, cancellationToken);
            return FromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(string id, CancellationToken cancellationToken)
        {
            var result = await _supplierService.DeleteAsync(id, cancellationToken);
            return FromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers(CancellationToken cancellationToken)
        {
            var result = await _supplierService.GetSuppliersAsync(cancellationToken: cancellationToken);
            return FromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplierById(string id, CancellationToken cancellationToken)
        {
            var result = await _supplierService.GetSupplierByIdAsync(id, cancellationToken: cancellationToken);
            return FromResult(result);
        }
    }
}
