namespace AgroShop.Application.Dto.SubCategoryDto
{
    public class SubCategoryDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = default!;

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}
