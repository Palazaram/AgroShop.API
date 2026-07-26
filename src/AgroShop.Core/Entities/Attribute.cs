using AgroShop.Core.Enums;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.Entities
{
    public class Attribute
    {
        private Attribute() { }

        public Guid Id { get; private set; }

        public AttributeName Name { get; private set; } = default!;
        public AttributeValueType ValueType { get; private set; }

        public virtual ICollection<ProductAttribute> ProductAttributes { get; private set; } = new List<ProductAttribute>();
        public virtual ICollection<AttributeOption> Options { get; private set; } = new List<AttributeOption>();

        public static Result<Attribute, Error> Create(string name, AttributeValueType valueType)
        {
            return AttributeName.Create(name)
                .Map(attributeName => new Attribute
                {
                    Id = Guid.CreateVersion7(),
                    Name = attributeName,
                    ValueType = valueType
                });
        }

        // ValueType is deliberately not editable here - changing it after
        // options/values already exist under the old type would leave the
        // existing data (e.g. multiple values recorded for what used to be
        // SingleSelect) meaningless without a separate migration of that data.
        public Result<Attribute, Error> Update(string name)
        {
            return AttributeName.Create(name)
                .Tap(attributeName => Name = attributeName)
                .Map(_ => this);
        }
    }
}
