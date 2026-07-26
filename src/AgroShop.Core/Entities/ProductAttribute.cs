using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.Entities
{
    public class ProductAttribute
    {
        private ProductAttribute() { }

        public Guid Id { get; private set; }

        public Guid SubCategoryId { get; private set; }
        public virtual SubCategory SubCategory { get; private set; } = null!;

        public Guid AttributeId { get; private set; }
        public virtual Attribute Attribute { get; private set; } = null!;

        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; private set; } = new List<ProductAttributeValue>();

        public static Result<ProductAttribute, Error> Create(Guid subCategoryId, Guid attributeId)
        {
            if (subCategoryId == Guid.Empty)
                return Result.Failure<ProductAttribute, Error>(Errors.General.ValueIsRequired("Підкатегорія"));

            if (attributeId == Guid.Empty)
                return Result.Failure<ProductAttribute, Error>(Errors.General.ValueIsRequired("Атрибут"));

            return Result.Success<ProductAttribute, Error>(new ProductAttribute
            {
                Id = Guid.CreateVersion7(),
                SubCategoryId = subCategoryId,
                AttributeId = attributeId
            });
        }

        // No Update - this row's entire identity is the (SubCategory, Attribute)
        // pair; "editing" either half makes it a different link, not a change
        // to this one. Re-pointing it in place would also silently reinterpret
        // every ProductAttributeValue already recorded against it.
    }
}
