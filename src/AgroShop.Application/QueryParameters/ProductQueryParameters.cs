namespace AgroShop.Application.QueryParameters
{
    public class ProductQueryParameters
    {
        public string? Search { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? SupplierId { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
