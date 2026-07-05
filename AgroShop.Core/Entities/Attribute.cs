using AgroShop.Core.ValueObjects;

namespace AgroShop.Core.Entities
{
    public class Attribute
    {
        private Attribute() { }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = default!;

        public virtual ICollection<ProductAttribute> ProductAttributes { get; private set; } = new List<ProductAttribute>();

        public static Attribute Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва атрибуту не може бути порожньою.", nameof(name));

            return new Attribute
            {
                Id = Guid.NewGuid(),
                Name = name.Trim()
            };
        }

        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва атрибуту не може бути порожньою.", nameof(name));

            Name = name.Trim();
        }
    }
}
