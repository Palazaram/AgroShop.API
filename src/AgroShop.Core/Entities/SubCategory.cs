using AgroShop.Core.ValueObjects;

namespace AgroShop.Core.Entities
{
    public class SubCategory
    {
        private SubCategory() { }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;

        public Guid CategoryId { get; private set; }
        public virtual Category Category { get; private set; } = null!;

        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
        public virtual ICollection<ProductAttribute> ProductAttributes { get; private set; } = new List<ProductAttribute>();

        public static SubCategory Create(string name, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва підкатегорії не може бути порожньою.", nameof(name));

            if (categoryId == Guid.Empty)
                throw new ArgumentException("Категорія є обов’язковою.", nameof(categoryId));

            return new SubCategory
            {
                Id = Guid.CreateVersion7(),
                Name = name.Trim(),
                CategoryId = categoryId
            };
        }

        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва підкатегорії не може бути порожньою.", nameof(name));

            Name = name.Trim();
        }
    }
}
