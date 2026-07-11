using AgroShop.Core.ValueObjects;

namespace AgroShop.Core.Entities
{
    public class Category
    {
        private Category() { }
        private readonly List<SubCategory> _subCategories = new();

        public Guid Id { get; private set; }
        public CategoryName Name { get; private set; } = default!;

        public IReadOnlyCollection<SubCategory> SubCategories => _subCategories;

        public static Category Create(CategoryName name)
        {
            return new Category
            {
                Id = Guid.NewGuid(),
                Name = name
            };
        }

        public void Update(CategoryName name)
        {
            Name = name;
        }
    }
}
