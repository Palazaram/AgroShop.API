using Microsoft.AspNetCore.Http;

namespace AgroShop.Application.Dto.CategoryDto
{
    public class AddCategoryDto
    {
        public required string Name { get; set; }
        public required IFormFile Image { get; set; }
    }
}
