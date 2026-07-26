using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.Entities
{
    public class Product
    {
        private Product() { }

        public Guid Id { get; private set; }

        public ProductName Name { get; private set; } = default!;
        public ProductDescription Description { get; private set; } = default!;
        public Money Price { get; private set; } = default!;
        public Sku Sku { get; private set; } = default!;
        public StockQuantity StockQuantity { get; private set; } = default!;

        public bool IsActive { get; private set; } = true;

        // Not stored - purely derived from stock. See ProductConfiguration's Ignore().
        public bool IsAvailable => StockQuantity.Value > 0;

        public string ImagePath { get; private set; } = default!;

        public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedUtc { get; private set; }

        public Guid SubCategoryId { get; private set; }
        public virtual SubCategory SubCategory { get; private set; } = null!;

        public Guid SupplierId { get; private set; }
        public virtual Supplier Supplier { get; private set; } = null!;

        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; private set; } = new List<ProductAttributeValue>();

        public static Result<Product, Error> Create(
            string name,
            string description,
            decimal price,
            string sku,
            int stockQuantity,
            Guid subCategoryId,
            Guid supplierId,
            string imagePath)
        {
            if (subCategoryId == Guid.Empty)
                return Result.Failure<Product, Error>(Errors.General.ValueIsRequired("Підкатегорія"));

            if (supplierId == Guid.Empty)
                return Result.Failure<Product, Error>(Errors.General.ValueIsRequired("Постачальник"));

            return ProductName.Create(name)
                .Bind(productName => ProductDescription.Create(description)
                    .Bind(productDescription => Sku.Create(sku)
                        .Bind(skuVo => Money.Create(price)
                            .Bind(money => StockQuantity.Create(stockQuantity)
                                .Map(stock => new Product
                                {
                                    Id = Guid.CreateVersion7(),
                                    Name = productName,
                                    Description = productDescription,
                                    Price = money,
                                    Sku = skuVo,
                                    StockQuantity = stock,
                                    SubCategoryId = subCategoryId,
                                    SupplierId = supplierId,
                                    ImagePath = imagePath,
                                    IsActive = true
                                })))));
        }

        public Result<Product, Error> Update(
            string name,
            string description,
            decimal price,
            string sku,
            int stockQuantity,
            Guid subCategoryId,
            Guid supplierId,
            bool isActive,
            string? imagePath = null)
        {
            if (subCategoryId == Guid.Empty)
                return Result.Failure<Product, Error>(Errors.General.ValueIsRequired("Підкатегорія"));

            if (supplierId == Guid.Empty)
                return Result.Failure<Product, Error>(Errors.General.ValueIsRequired("Постачальник"));

            return ProductName.Create(name)
                .Bind(productName => ProductDescription.Create(description)
                    .Bind(productDescription => Sku.Create(sku)
                        .Bind(skuVo => Money.Create(price)
                            .Bind(money => StockQuantity.Create(stockQuantity)
                                .Tap(stock =>
                                {
                                    Name = productName;
                                    Description = productDescription;
                                    Price = money;
                                    Sku = skuVo;
                                    StockQuantity = stock;
                                    IsActive = isActive;
                                    UpdatedUtc = DateTime.UtcNow;

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

                                    // Only overwrite when a new image was actually uploaded
                                    // (same convention as Category.Update).
                                    if (!string.IsNullOrWhiteSpace(imagePath))
                                        ImagePath = imagePath;
                                })
                                .Map(_ => this)))));
        }
    }
}
