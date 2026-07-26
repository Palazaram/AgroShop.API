namespace AgroShop.Application.Dto.ProductDto
{
    public class ProductFiltersDto
    {
        public List<ProductFilterGroupDto> AttributeGroups { get; set; } = [];
        public List<ProductFilterSupplierOptionDto> SupplierOptions { get; set; } = [];
    }
}
