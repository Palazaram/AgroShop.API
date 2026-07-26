using AgroShop.Application.Dto.ProductDto;
using AgroShop.Core.Entities;

namespace AgroShop.Application.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name.Value,
                Description = product.Description.Value,
                Price = product.Price.Value,
                Sku = product.Sku.Value,
                StockQuantity = product.StockQuantity.Value,
                IsActive = product.IsActive,
                IsAvailable = product.IsAvailable,
                ImagePath = product.ImagePath,
                CreatedAtUtc = product.CreatedAtUtc,
                UpdatedUtc = product.UpdatedUtc,
                SubCategoryId = product.SubCategoryId,
                SubCategoryName = product.SubCategory.Name.Value,
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier.Name.Value
            };
        }

        public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> products)
        {
            return products.Select(p => p.ToDto());
        }
    }
}
