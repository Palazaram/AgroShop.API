using AgroShop.Core.Enums;

namespace AgroShop.Application.Dto.AttributeDto
{
    public class AddAttributeDto
    {
        public required string Name { get; set; }
        public required AttributeValueType ValueType { get; set; }
    }
}
