using AgroShop.Core.ValueObjects;

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

        public string Value { get; private set; } = default!;

        public static ProductAttributeValue Create(string value, Guid productAttributeId, Guid productId)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Значення атрибуту не може бути порожнім.", nameof(value));

            if (productAttributeId == Guid.Empty)
                throw new ArgumentException("Атрибут є обов’язковим.", nameof(productAttributeId));

            if (productId == Guid.Empty)
                throw new ArgumentException("Продукт є обов’язковим.", nameof(productId));

            return new ProductAttributeValue
            {
                Id = Guid.NewGuid(),
                Value = value.Trim(),
                ProductAttributeId = productAttributeId,
                ProductId = productId
            };
        }

        public void Update(string value, Guid productAttributeId, Guid productId)
        {
            if (ProductAttributeId != productAttributeId)
            {
                ProductAttributeId = productAttributeId;
                ProductAttribute = null!;
            }

            if (ProductId != productId)
            {
                ProductId = productId;
                Product = null!;
            }

            Value = value.Trim();
        }

    }
}
