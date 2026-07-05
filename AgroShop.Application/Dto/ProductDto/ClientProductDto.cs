namespace AgroShop.Application.Dto.ProductDto
{
    public class ClientProductDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = default!;
        public string? Description { get; init; }
        public decimal Price { get; init; }
        public string ImagePath { get; init; } = default!;
        public bool IsAvailable { get; init; }

        public string CategoryName { get; init; } = default!;
        public string SubCategoryName { get; init; } = default!;
        public string SupplierName { get; init; } = default!;
    }
}
