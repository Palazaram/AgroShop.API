namespace AgroShop.Application.Dto.AttributeOptionDto
{
    public class AddAttributeOptionDto
    {
        public required string Value { get; set; }
        public required Guid AttributeId { get; set; }
    }
}
