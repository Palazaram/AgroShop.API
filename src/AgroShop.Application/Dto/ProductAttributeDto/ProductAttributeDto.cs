namespace AgroShop.Application.Dto.ProductAttributeDto
{
    public class ProductAttributeDto
    {
        public Guid Id { get; set; }

        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = default!;

        public Guid AttributeId { get; set; }
        public string AttributeName { get; set; } = default!;
    }
}
