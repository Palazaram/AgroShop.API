namespace AgroShop.Application.Dto.ProductDto
{
    public class ProductDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public string Sku { get; set; } = default!;
        public int StockQuantity { get; set; }
        public decimal PackageAmount { get; set; }
        public string PackageUnit { get; set; } = default!;

        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }

        public string ImagePath { get; set; } = default!;

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }

        // The category is the sub-category's parent, carried here so a client
        // can show where the product sits without a second round trip. Both
        // levels travel together because the trail needs both.
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;

        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = default!;

        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; } = default!;

        public List<ProductAttributeValueDto> AttributeValues { get; set; } = [];
    }
}
