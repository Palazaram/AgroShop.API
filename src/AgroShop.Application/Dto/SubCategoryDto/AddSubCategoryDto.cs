namespace AgroShop.Application.Dto.SubCategoryDto
{
    public class AddSubCategoryDto
    {
        public required string Name { get; set; }
        public required Guid CategoryId { get; set; }
    }
}
