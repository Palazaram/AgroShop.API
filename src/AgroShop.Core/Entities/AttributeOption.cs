using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.Entities
{
    public class AttributeOption
    {
        private AttributeOption() { }

        public Guid Id { get; private set; }

        public AttributeOptionValue Value { get; private set; } = default!;

        public Guid AttributeId { get; private set; }
        public virtual Attribute Attribute { get; private set; } = null!;

        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; private set; } = new List<ProductAttributeValue>();

        // AttributeId is not reassignable after creation - an option "belongs"
        // to the attribute it was defined under for good (e.g. "Амброзія"
        // only ever makes sense as a "Бур'ян" option); moving it to a
        // different attribute would silently reinterpret every existing
        // ProductAttributeValue that already points at it.
        public static Result<AttributeOption, Error> Create(string value, Guid attributeId)
        {
            if (attributeId == Guid.Empty)
                return Result.Failure<AttributeOption, Error>(Errors.General.ValueIsRequired("Атрибут"));

            return AttributeOptionValue.Create(value)
                .Map(optionValue => new AttributeOption
                {
                    Id = Guid.CreateVersion7(),
                    Value = optionValue,
                    AttributeId = attributeId
                });
        }

        public Result<AttributeOption, Error> Update(string value)
        {
            return AttributeOptionValue.Create(value)
                .Tap(optionValue => Value = optionValue)
                .Map(_ => this);
        }
    }
}
