namespace AgroShop.Application.Dto.ProductDto
{
    public class ProductFilterSupplierOptionDto
    {
        public Guid SupplierId { get; set; }
        public string Name { get; set; } = default!;
        public int ProductCount { get; set; }
    }
}
