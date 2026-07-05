using AgroShop.Core.ValueObjects;

namespace AgroShop.Core.Entities
{
    public class Supplier
    {
        private Supplier() { }

        public Guid Id { get; private set; }
        public string Name { get; private set; } = default!;

        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();

        public static Supplier Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва постачальника не може бути порожньою");

            return new Supplier
            {
                Id = Guid.NewGuid(),
                Name = name.Trim()
            };
        }

        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Назва постачальника не може бути порожньою");

            Name = name.Trim();
        }
    }
}
