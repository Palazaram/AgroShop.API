namespace AgroShop.Application.Dto.ProductDto
{
    public class ProductFilterPackageOptionDto
    {
        // Packaging is one filter dimension but two columns - 5 г and 5 кг are
        // different options - so it travels as a single composite key. The
        // server hands the key back here rather than expecting the client to
        // assemble "amount:unit" itself, so the format stays an
        // implementation detail of ProductService.
        public string Key { get; set; } = default!;

        public decimal PackageAmount { get; set; }
        public string PackageUnit { get; set; } = default!;
        public int ProductCount { get; set; }
    }
}
