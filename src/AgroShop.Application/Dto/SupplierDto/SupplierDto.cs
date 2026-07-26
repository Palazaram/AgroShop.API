namespace AgroShop.Application.Dto.SupplierDto
{
    public class SupplierDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;
        public string? ImagePath { get; set; }
    }
}
