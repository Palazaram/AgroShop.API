namespace AgroShop.Application.Dto.ProductAttributeDto
{
    public class AddProductAttributeDto
    {
        public required Guid SubCategoryId { get; set; }
        public required Guid AttributeId { get; set; }
    }
}
