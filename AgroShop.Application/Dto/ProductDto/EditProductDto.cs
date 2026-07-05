using Microsoft.AspNetCore.Http;

namespace AgroShop.Application.Dto.ProductDto
{
    public class EditProductDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? SupplierId { get; set; }
        public string ImagePath { get; set; } = default!;
        public bool IsAvailable { get; set; } = default!;

        public IFormFile? Image { get; set; }
    }
}
