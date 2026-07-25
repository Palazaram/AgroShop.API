namespace AgroShop.Application.Dto.CategoryDto
{
    public class CategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public string ImagePath { get; set; } = default!;
    }
}
