namespace AgroShop.Application.Dto.ProductDto
{
    public class ProductAttributeValueDto
    {
        public Guid AttributeOptionId { get; set; }
        public Guid AttributeId { get; set; }
        public string AttributeName { get; set; } = default!;
        public string Value { get; set; } = default!;
    }
}
