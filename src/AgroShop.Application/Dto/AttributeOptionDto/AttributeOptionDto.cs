namespace AgroShop.Application.Dto.AttributeOptionDto
{
    public class AttributeOptionDto
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = default!;

        public Guid AttributeId { get; set; }
        public string AttributeName { get; set; } = default!;
    }
}
