using AgroShop.Application.Dto.AttributeOptionDto;
using AgroShop.Core.Entities;

namespace AgroShop.Application.Mappers
{
    public static class AttributeOptionMapper
    {
        public static AttributeOptionDto ToDto(this AttributeOption option)
        {
            return new AttributeOptionDto
            {
                Id = option.Id,
                Value = option.Value.Value,
                AttributeId = option.AttributeId,
                AttributeName = option.Attribute.Name.Value
            };
        }

        public static IEnumerable<AttributeOptionDto> ToDto(this IEnumerable<AttributeOption> options)
        {
            return options.Select(o => o.ToDto());
        }
    }
}
