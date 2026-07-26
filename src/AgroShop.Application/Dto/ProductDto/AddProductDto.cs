using AgroShop.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace AgroShop.Application.Dto.ProductDto
{
    public class AddProductDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string Sku { get; set; }
        public required int StockQuantity { get; set; }
        public required decimal PackageAmount { get; set; }
        public required PackageUnit PackageUnit { get; set; }
        public required Guid SubCategoryId { get; set; }
        public required Guid SupplierId { get; set; }
        public required IFormFile Image { get; set; }
    }
}
