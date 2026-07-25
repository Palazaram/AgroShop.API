using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.Entities
{
    public class SubCategory
    {
        private SubCategory() { }

        public Guid Id { get; private set; }

        public SubCategoryName Name { get; private set; } = default!;

        public Guid CategoryId { get; private set; }
        public virtual Category Category { get; private set; } = null!;

        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();
        public virtual ICollection<ProductAttribute> ProductAttributes { get; private set; } = new List<ProductAttribute>();

        public static Result<SubCategory, Error> Create(string name, Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                return Result.Failure<SubCategory, Error>(Errors.General.ValueIsRequired("Категорія"));

            return SubCategoryName.Create(name)
                .Map(subCategoryName => new SubCategory
                {
                    Id = Guid.CreateVersion7(),
                    Name = subCategoryName,
                    CategoryId = categoryId
                });
        }

        // CategoryId is editable here (unlike Category, which has no parent to
        // reassign) - SubCategories already carry Products/ProductAttributes,
        // so re-parenting one shouldn't require deleting and recreating it.
        public Result<SubCategory, Error> Update(string name, Guid categoryId)
        {
            if (categoryId == Guid.Empty)
                return Result.Failure<SubCategory, Error>(Errors.General.ValueIsRequired("Категорія"));

            return SubCategoryName.Create(name)
                .Tap(subCategoryName => Name = subCategoryName)
                .Tap(_ =>
                {
                    if (CategoryId != categoryId)
                    {
                        CategoryId = categoryId;
                        Category = null!;
                    }
                })
                .Map(_ => this);
        }
    }
}
