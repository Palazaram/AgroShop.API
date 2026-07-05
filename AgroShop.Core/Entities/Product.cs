using AgroShop.Core.ValueObjects;

namespace AgroShop.Core.Entities
{
    public class Product
    {
        private Product() { }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;
        public string ImagePath { get; private set; } = default!;
        public string? Description { get; private set; }

        public decimal Price { get; private set; }

        public bool IsAvailable { get; private set; } = true;

        public Guid SubCategoryId { get; private set; }
        public virtual SubCategory SubCategory { get; private set; } = null!;

        public Guid SupplierId { get; private set; }
        public virtual Supplier Supplier { get; private set; } = null!;

        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; private set; } = new List<ProductAttributeValue>();

        public static Product Create
            (
                string name,
                string? description,
                decimal price,
                Guid subCategoryId,
                Guid supplierId,
                string imagePath
            )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва не може бути порожньою");

            if (price <= 0)
                throw new ArgumentException("Ціна повинна бути більшою за нуль");

            return new Product
            {
                Id = Guid.NewGuid(),
                Name = name.Trim(),
                Description = description?.Trim(),
                Price = price,
                SubCategoryId = subCategoryId,
                SupplierId = supplierId,
                ImagePath = imagePath,
                IsAvailable = true
            };
        }

        public void Update
            (
                string name,
                string? description,
                decimal price,
                Guid subCategoryId,
                Guid supplierId,
                bool isAvailable,
                string? imagePath = null // теперь опциональный
            )
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва не може бути порожньою");

            if (price <= 0)
                throw new ArgumentException("Ціна повинна бути більшою за нуль");

            Name = name.Trim();
            Description = description?.Trim();
            Price = price;
            IsAvailable = isAvailable;

            if (SubCategoryId != subCategoryId)
            {
                SubCategoryId = subCategoryId;
                SubCategory = null!;
            }

            if (SupplierId != supplierId)
            {
                SupplierId = supplierId;
                Supplier = null!;
            }

            if (!string.IsNullOrWhiteSpace(imagePath))
            {
                ImagePath = imagePath.Trim(); // установка возможна только внутри метода
            }
        }
    }
}
