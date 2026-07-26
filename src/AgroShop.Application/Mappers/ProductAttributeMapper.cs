using AgroShop.Application.Dto.ProductAttributeDto;
using AgroShop.Core.Entities;

namespace AgroShop.Application.Mappers
{
    public static class ProductAttributeMapper
    {
        public static ProductAttributeDto ToDto(this ProductAttribute productAttribute)
        {
            return new ProductAttributeDto
            {
                Id = productAttribute.Id,
                SubCategoryId = productAttribute.SubCategoryId,
                SubCategoryName = productAttribute.SubCategory.Name.Value,
                AttributeId = productAttribute.AttributeId,
                AttributeName = productAttribute.Attribute.Name.Value
            };
        }

        public static IEnumerable<ProductAttributeDto> ToDto(this IEnumerable<ProductAttribute> productAttributes)
        {
            return productAttributes.Select(pa => pa.ToDto());
        }
    }
}
