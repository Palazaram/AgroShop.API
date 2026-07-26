using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.Entities
{
    public class ProductAttributeValue
    {
        private ProductAttributeValue() { }

        public Guid Id { get; private set; }

        public Guid ProductAttributeId { get; private set; }
        public virtual ProductAttribute ProductAttribute { get; private set; } = null!;

        public Guid ProductId { get; private set; }
        public virtual Product Product { get; private set; } = null!;

        public Guid AttributeOptionId { get; private set; }
        public virtual AttributeOption AttributeOption { get; private set; } = null!;

        public static Result<ProductAttributeValue, Error> Create(Guid productAttributeId, Guid productId, Guid attributeOptionId)
        {
            if (productAttributeId == Guid.Empty)
                return Result.Failure<ProductAttributeValue, Error>(Errors.General.ValueIsRequired("Атрибут"));

            if (productId == Guid.Empty)
                return Result.Failure<ProductAttributeValue, Error>(Errors.General.ValueIsRequired("Товар"));

            if (attributeOptionId == Guid.Empty)
                return Result.Failure<ProductAttributeValue, Error>(Errors.General.ValueIsRequired("Варіант атрибуту"));

            return Result.Success<ProductAttributeValue, Error>(new ProductAttributeValue
            {
                Id = Guid.CreateVersion7(),
                ProductAttributeId = productAttributeId,
                ProductId = productId,
                AttributeOptionId = attributeOptionId
            });
        }

        // No Update - this row's identity is the (Product, ProductAttribute,
        // AttributeOption) triple. For MultiSelect attributes a product can
        // have several of these rows at once; changing the selection means
        // deleting some rows and creating others, not editing one in place.
    }
}
