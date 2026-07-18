using AgroShop.Core.ValueObjects;

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

        public static ProductAttribute Create(Guid subCategoryId, Guid attributeId)
        {
            if (subCategoryId == Guid.Empty)
                throw new ArgumentException("Підкатегорія є обов’язковою.", nameof(subCategoryId));

            if (attributeId == Guid.Empty)
                throw new ArgumentException("Атрибут є обов’язковим.", nameof(attributeId));

            return new ProductAttribute
            {
                Id = Guid.CreateVersion7(),
                SubCategoryId = subCategoryId,
                AttributeId = attributeId
            };
        }

        public void Update(Guid subCategoryId, Guid attributeId)
        {
            if (SubCategoryId != subCategoryId)
            {
                SubCategoryId = subCategoryId;
                SubCategory = null!;
            }

            if (AttributeId != attributeId)
            {
                AttributeId = attributeId;
                Attribute = null!;
            }
        }
    }
}
