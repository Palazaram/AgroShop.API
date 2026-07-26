using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.Entities
{
    public class Supplier
    {
        private Supplier() { }

        public Guid Id { get; private set; }
        public SupplierName Name { get; private set; } = default!;
        public string? ImagePath { get; private set; }

        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();

        public static Result<Supplier, Error> Create(string name, string? imagePath = null)
        {
            return SupplierName.Create(name)
                .Map(supplierName => new Supplier
                {
                    Id = Guid.CreateVersion7(),
                    Name = supplierName,
                    ImagePath = imagePath
                });
        }

        public Result<Supplier, Error> Update(string name, string? imagePath = null)
        {
            return SupplierName.Create(name)
                .Tap(supplierName => Name = supplierName)
                .Tap(_ =>
                {
                    // Only overwrite when a new image was actually uploaded (same convention as Category.Update).
                    if (!string.IsNullOrWhiteSpace(imagePath))
                        ImagePath = imagePath;
                })
                .Map(_ => this);
        }
    }
}
