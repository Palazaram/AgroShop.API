using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Core.Entities;

namespace AgroShop.Application.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name.Value,
                ImagePath = category.ImagePath
            };
        }

        public static IEnumerable<CategoryDto> ToDto(this IEnumerable<Category> categories)
        {
            return categories.Select(c => c.ToDto());
        }
    }
}
