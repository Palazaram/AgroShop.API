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

        public bool IsActive { get; set; }
        public bool IsAvailable { get; set; }

        public string ImagePath { get; set; } = default!;

        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedUtc { get; set; }

        public Guid SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = default!;

        public Guid SupplierId { get; set; }
        public string SupplierName { get; set; } = default!;
    }
}
