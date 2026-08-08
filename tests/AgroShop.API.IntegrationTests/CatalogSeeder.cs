using AgroShop.Core.Entities;
using AgroShop.Core.Enums;
using AgroShop.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AgroShop.API.IntegrationTests
{
    // Seeds through the domain factories and the DbContext directly, not
    // through the HTTP write endpoints - the factories enforce the same
    // invariants (Ukrainian-only product names, SKU format, positive money),
    // so the data is honest, without dragging auth and multipart uploads
    // into read-path tests.
    //
    // Seeding bypasses the services, so it also bypasses their cache
    // invalidation: always seed BEFORE the first listing request of a test,
    // never after, or the listing may serve a cached pre-seed answer.
    public static class CatalogSeeder
    {
        public sealed record SeededCatalog(
            Category Category,
            SubCategory SubCategory,
            Supplier Supplier,
            IReadOnlyList<Product> Products);

        public static async Task<SeededCatalog> SeedBasicCatalogAsync(ApiFixture api)
        {
            using var scope = api.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AgroShopDbContext>();

            var category = Category.Create("Насіння", "/images/categories/seeds.jpg").Value;
            var subCategory = SubCategory.Create("Насіння томатів", category.Id).Value;
            var supplier = Supplier.Create("Сімейний сад").Value;

            var products = new List<Product>
            {
                CreateProduct("Насіння томату Черрі", "TOM-CHERRY-1", 45.50m, subCategory.Id, supplier.Id),
                CreateProduct("Насіння томату Де Барао", "TOM-DEBARAO-1", 38.00m, subCategory.Id, supplier.Id),
                CreateProduct("Насіння томату Сан Марцано", "TOM-SANMARZ-1", 52.25m, subCategory.Id, supplier.Id),
            };

            db.Add(category);
            db.Add(subCategory);
            db.Add(supplier);
            db.AddRange(products);
            await db.SaveChangesAsync();

            return new SeededCatalog(category, subCategory, supplier, products);
        }

        private static Product CreateProduct(string name, string sku, decimal price, Guid subCategoryId, Guid supplierId) =>
            Product.Create(
                name,
                "Перевірене насіння для відкритого ґрунту та теплиць, високий відсоток схожості.",
                price,
                sku,
                stockQuantity: 50,
                packageAmount: 5,
                PackageUnit.Gram,
                subCategoryId,
                supplierId,
                "/images/products/seeded.jpg").Value;
    }
}
