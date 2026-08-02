namespace AgroShop.Application.Dto.ProductDto
{
    // Every facet's counts are computed with the standard faceted-search
    // "self-exclude" rule: a facet reflects every OTHER currently active
    // filter, but ignores its own - so checking a supplier tells you how
    // many results *each other* option would additionally narrow to,
    // instead of freezing at the count from before you picked anything.
    public class ProductFacetsDto
    {
        public List<ProductFilterSubCategoryOptionDto> SubCategoryOptions { get; set; } = [];
        public List<ProductFilterSupplierOptionDto> SupplierOptions { get; set; } = [];
        public List<ProductFilterGroupDto> AttributeGroups { get; set; } = [];
    }
}
