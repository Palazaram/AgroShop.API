using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.Entities
{
    public class Category
    {
        private Category() { }
        private readonly List<SubCategory> _subCategories = new();

        public Guid Id { get; private set; }
        public CategoryName Name { get; private set; } = default!;
        public string ImagePath { get; private set; } = default!;

        public IReadOnlyCollection<SubCategory> SubCategories => _subCategories;

        public static Result<Category, Error> Create(string name, string imagePath)
        {
            return CategoryName.Create(name)
                .Map(categoryName => new Category
                {
                    Id = Guid.CreateVersion7(),
                    Name = categoryName,
                    ImagePath = imagePath
                });
        }

        public Result<Category, Error> Update(string name, string? imagePath = null)
        {
            return CategoryName.Create(name)
                .Tap(categoryName => Name = categoryName)
                .Tap(_ =>
                {
                    // Only overwrite when a new image was actually uploaded (same convention as Product.Update).
                    if (!string.IsNullOrWhiteSpace(imagePath))
                        ImagePath = imagePath;
                })
                .Map(_ => this);
        }
    }
}
