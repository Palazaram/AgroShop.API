namespace AgroShop.Application.Dto.ProductDto
{
    public class ProductFilterOptionDto
    {
        public Guid AttributeOptionId { get; set; }
        public string Value { get; set; } = default!;
        public int ProductCount { get; set; }
    }
}
