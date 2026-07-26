namespace AgroShop.Application.Dto.AttributeDto
{
    public class AttributeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string ValueType { get; set; } = default!;

        public List<AgroShop.Application.Dto.AttributeOptionDto.AttributeOptionDto> Options { get; set; } = [];
    }
}
