using AgroShop.Application.Dto.SubCategoryDto;
using AgroShop.Core.Entities;

namespace AgroShop.Application.Mappers
{
    public static class SubCategoryMapper
    {
        public static SubCategoryDto ToDto(this SubCategory subCategory)
        {
            return new SubCategoryDto
            {
                Id = subCategory.Id,
                Name = subCategory.Name.Value,
                CategoryId = subCategory.CategoryId,
                CategoryName = subCategory.Category.Name.Value
            };
        }

        public static IEnumerable<SubCategoryDto> ToDto(this IEnumerable<SubCategory> subCategories)
        {
            return subCategories.Select(sc => sc.ToDto());
        }
    }
}
