namespace AgroShop.Application.Dto.ProductDto
{
    // Every facet's counts are computed with the standard faceted-search
    // "self-exclude" rule: a facet reflects every OTHER currently active
    // filter, but ignores its own - so checking a supplier tells you how
    // many results *each other* option would additionally narrow to,
    // instead of freezing at the count from before you picked anything.
    public class ProductFacetsDto
    {
        // How many products match the filters exactly as passed in - the one
        // number here that is NOT self-excluded. Lets a caller previewing an
        // uncommitted selection ("Показати N товарів") get the count from this
        // same request instead of a second round trip.
        public int TotalCount { get; set; }

        public List<ProductFilterSubCategoryOptionDto> SubCategoryOptions { get; set; } = [];
        public List<ProductFilterSupplierOptionDto> SupplierOptions { get; set; } = [];
        public List<ProductFilterPackageOptionDto> PackageOptions { get; set; } = [];
        public List<ProductFilterGroupDto> AttributeGroups { get; set; } = [];
    }
}
