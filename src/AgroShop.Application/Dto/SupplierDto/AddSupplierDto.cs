using Microsoft.AspNetCore.Http;

namespace AgroShop.Application.Dto.SupplierDto
{
    public class AddSupplierDto
    {
        public required string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
