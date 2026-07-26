using AgroShop.Application.Dto.AttributeDto;
using Attribute = AgroShop.Core.Entities.Attribute;

namespace AgroShop.Application.Mappers
{
    public static class AttributeMapper
    {
        public static AttributeDto ToDto(this Attribute attribute)
        {
            return new AttributeDto
            {
                Id = attribute.Id,
                Name = attribute.Name.Value,
                ValueType = attribute.ValueType.ToString(),
                Options = attribute.Options.Select(o => o.ToDto()).ToList()
            };
        }

        public static IEnumerable<AttributeDto> ToDto(this IEnumerable<Attribute> attributes)
        {
            return attributes.Select(a => a.ToDto());
        }
    }
}
