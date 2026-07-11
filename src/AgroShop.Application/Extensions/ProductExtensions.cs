using AgroShop.Application.Dto.ProductDto;
using AgroShop.Core.Entities;

namespace AgroShop.Application.Extensions
{
    public static class ProductExtensions
    {
        public static ClientProductDto ToClientDto(this Product product)
        {
            return new ClientProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ImagePath = product.ImagePath,
                IsAvailable = product.IsAvailable,
                CategoryName = product.SubCategory?.Category?.Name.Value ?? string.Empty,
                SubCategoryName = product.SubCategory?.Name ?? string.Empty,
                SupplierName = product.Supplier?.Name ?? string.Empty
            };
        }
    }
}
