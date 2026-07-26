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

        public virtual ICollection<Product> Products { get; private set; } = new List<Product>();

        public static Result<Supplier, Error> Create(string name)
        {
            return SupplierName.Create(name)
                .Map(supplierName => new Supplier
                {
                    Id = Guid.CreateVersion7(),
                    Name = supplierName
                });
        }

        public Result<Supplier, Error> Update(string name)
        {
            return SupplierName.Create(name)
                .Tap(supplierName => Name = supplierName)
                .Map(_ => this);
        }
    }
}
