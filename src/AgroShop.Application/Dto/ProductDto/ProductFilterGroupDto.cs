namespace AgroShop.Application.Dto.ProductDto
{
    public class ProductFilterGroupDto
    {
        public Guid AttributeId { get; set; }
        public string AttributeName { get; set; } = default!;
        public string ValueType { get; set; } = default!;

        public List<ProductFilterOptionDto> Options { get; set; } = [];
    }
}
