using AgroShop.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Attribute = AgroShop.Core.Entities.Attribute;

namespace AgroShop.Persistence.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<Product> IncludeAll(this IQueryable<Product> query)
        {
            return query
                   .Include(p => p.SubCategory).ThenInclude(sc => sc.Category)
                   .Include(p => p.Supplier);
        }

        public static IQueryable<Category> IncludeAll(this IQueryable<Category> query)
        {
            return query
                   .Include(c => c.SubCategories);
        }

        public static IQueryable<SubCategory> IncludeAll(this IQueryable<SubCategory> query)
        {
            return query
                   .Include(c => c.Category);
        }

        public static IQueryable<Supplier> IncludeAll(this IQueryable<Supplier> query)
        {
            return query
                   .Include(s => s.Products);
        }

        public static IQueryable<User> IncludeAll(this IQueryable<User> query)
        {
            return query
                   .Include(u => u.Role)
                   .Include(s => s.RefreshTokens);
        }

        public static IQueryable<Attribute> IncludeAll(this IQueryable<Attribute> query)
        {
            return query
                   .Include(a => a.Options);
        }

        public static IQueryable<AttributeOption> IncludeAll(this IQueryable<AttributeOption> query)
        {
            return query
                   .Include(o => o.Attribute);
        }

        public static IQueryable<ProductAttribute> IncludeAll(this IQueryable<ProductAttribute> query)
        {
            return query
                   .Include(pa => pa.SubCategory)
                   .Include(pa => pa.Attribute);
        }
    }
}
