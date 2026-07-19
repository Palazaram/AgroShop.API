using Microsoft.AspNetCore.Http;

namespace AgroShop.Application.Dto.CategoryDto
{
    public class UpdateCategoryDto
    {
        public required string Name { get; set; }
        public IFormFile? Image { get; set; }
    }
}
