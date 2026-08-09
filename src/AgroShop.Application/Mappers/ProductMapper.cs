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
                PackageAmount = product.PackageSize.Amount,
                PackageUnit = product.PackageSize.Unit.ToString(),
                IsActive = product.IsActive,
                IsAvailable = product.IsAvailable,
                ImagePath = product.ImagePath,
                CreatedAtUtc = product.CreatedAtUtc,
                UpdatedUtc = product.UpdatedUtc,
                // Safe on every path that reaches this mapper: both the listing
                // queryable and the by-id query go through IncludeAll, which
                // loads SubCategory.Category, and lazy loading is off - so this
                // adds no queries and can't hit a null navigation.
                CategoryId = product.SubCategory.CategoryId,
                CategoryName = product.SubCategory.Category.Name.Value,
                SubCategoryId = product.SubCategoryId,
                SubCategoryName = product.SubCategory.Name.Value,
                SupplierId = product.SupplierId,
                SupplierName = product.Supplier.Name.Value,
                AttributeValues = product.ProductAttributeValues.Select(pav => new ProductAttributeValueDto
                {
                    AttributeOptionId = pav.AttributeOptionId,
                    AttributeId = pav.AttributeOption.AttributeId,
                    AttributeName = pav.AttributeOption.Attribute.Name.Value,
                    Value = pav.AttributeOption.Value.Value
                }).ToList()
            };
        }

        public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> products)
        {
            return products.Select(p => p.ToDto());
        }
    }
}
