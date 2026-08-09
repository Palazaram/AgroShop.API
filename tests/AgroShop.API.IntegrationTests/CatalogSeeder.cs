using AgroShop.Core.Entities;
using AgroShop.Core.Enums;
using AgroShop.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AgroShop.API.IntegrationTests
{
    // Seeds through the domain factories and the DbContext directly, not
    // through the HTTP write endpoints - the factories enforce the same
    // invariants (Ukrainian-only product names, SKU format, positive money),
    // so the data is honest, without dragging auth and multipart uploads
    // into read-path tests.
    //
    // Each seeder is idempotent (get-or-create by SKU): xunit builds a new
    // test-class instance per fact, so a class's InitializeAsync runs once
    // per test, against a database shared by the whole collection. Callers
    // must flush the cache afterwards (ApiFixture.ClearCache) because
    // seeding bypasses the services' cache invalidation.
    public static class CatalogSeeder
    {
        public sealed record SeededCatalog(
            Category Category,
            SubCategory SubCategory,
            Supplier Supplier,
            IReadOnlyList<Product> Products);

        // Its own sub-category, so the assertions can talk about "everything
        // here" without other test classes' data drifting into the answer.
        // Prices are spread evenly and the product under test is the cheapest,
        // which is what makes the ordering visible: nearest-by-price picks the
        // four just above it, while taking the first page by price descending
        // would pick the four most expensive instead.
        public sealed record VisibilityCatalog(
            SubCategory SubCategory,
            Product Cheapest,
            IReadOnlyList<Product> ActiveNeighbours,
            Product Hidden,
            Product MostExpensive);

        public sealed record SearchCatalog(
            Supplier CucumberSupplier,
            Supplier ZucchiniSupplier,
            Product MercuryEarly,
            Product MercuryLate,
            Product Jupiter,
            Product DescriptionTrap);

        public static async Task<SeededCatalog> SeedBasicCatalogAsync(ApiFixture api)
        {
            using var scope = api.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AgroShopDbContext>();

            var skus = new[] { "TOM-CHERRY-1", "TOM-DEBARAO-1", "TOM-SANMARZ-1" };
            var existing = await LoadBySkusAsync(db, skus);
            if (existing.Count == skus.Length)
            {
                var subCategory = await db.Set<SubCategory>().FirstAsync(sc => sc.Id == existing[0].SubCategoryId);
                var category = await db.Set<Category>().FirstAsync(c => c.Id == subCategory.CategoryId);
                var supplier = await db.Set<Supplier>().FirstAsync(s => s.Id == existing[0].SupplierId);
                return new SeededCatalog(category, subCategory, supplier, existing);
            }

            var newCategory = Category.Create("Насіння", "/images/categories/seeds.jpg").Value;
            var newSubCategory = SubCategory.Create("Насіння томатів", newCategory.Id).Value;
            var newSupplier = Supplier.Create("Сімейний сад").Value;

            var products = new List<Product>
            {
                CreateProduct("Насіння томату Черрі", skus[0], 45.50m, newSubCategory.Id, newSupplier.Id),
                CreateProduct("Насіння томату Де Барао", skus[1], 38.00m, newSubCategory.Id, newSupplier.Id),
                CreateProduct("Насіння томату Сан Марцано", skus[2], 52.25m, newSubCategory.Id, newSupplier.Id),
            };

            db.Add(newCategory);
            db.Add(newSubCategory);
            db.Add(newSupplier);
            db.AddRange(products);
            await db.SaveChangesAsync();

            return new SeededCatalog(newCategory, newSubCategory, newSupplier, products);
        }

        // A world for the search tests, built from name tokens ("меркурій",
        // "юпітер", "огірок", "кабачок") that no other seeder uses, so
        // assertions can count exact matches in the shared database. The
        // trap product carries "меркурій" only in its description - the one
        // field search must NOT match.
        public static async Task<SearchCatalog> SeedSearchCatalogAsync(ApiFixture api)
        {
            using var scope = api.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AgroShopDbContext>();

            var skus = new[] { "CUC-MERC-01", "CUC-MERC-02", "CUC-JUP-01", "ZUC-GRIB-01" };
            var existing = await LoadBySkusAsync(db, skus);
            if (existing.Count == skus.Length)
            {
                var suppliers = await db.Set<Supplier>()
                    .Where(s => existing.Select(p => p.SupplierId).Contains(s.Id))
                    .ToListAsync();
                return new SearchCatalog(
                    suppliers.First(s => s.Id == existing[0].SupplierId),
                    suppliers.First(s => s.Id == existing[3].SupplierId),
                    existing[0], existing[1], existing[2], existing[3]);
            }

            var category = Category.Create("Городина", "/images/categories/vegetables.jpg").Value;
            var subCategory = SubCategory.Create("Огірки та кабачки", category.Id).Value;
            var cucumberSupplier = Supplier.Create("Городина трейд").Value;
            var zucchiniSupplier = Supplier.Create("Овочевий дім").Value;

            var mercuryEarly = CreateProduct("Огірок Меркурій щедрий", skus[0], 10.00m, subCategory.Id, cucumberSupplier.Id);
            var mercuryLate = CreateProduct("Меркурій огірок пізній", skus[1], 20.00m, subCategory.Id, cucumberSupplier.Id);
            var jupiter = CreateProduct("Огірок Юпітер ранній", skus[2], 30.00m, subCategory.Id, zucchiniSupplier.Id);
            var descriptionTrap = Product.Create(
                "Кабачок Грибовський золотий",
                "У назві цього товару слова меркурій немає, воно згадується лише в описі для перевірки пошуку.",
                40.00m,
                skus[3],
                stockQuantity: 50,
                packageAmount: 5,
                PackageUnit.Gram,
                subCategory.Id,
                zucchiniSupplier.Id,
                "/images/products/seeded.jpg").Value;

            db.Add(category);
            db.Add(subCategory);
            db.Add(cucumberSupplier);
            db.Add(zucchiniSupplier);
            db.AddRange(mercuryEarly, mercuryLate, jupiter, descriptionTrap);
            await db.SaveChangesAsync();

            return new SearchCatalog(cucumberSupplier, zucchiniSupplier, mercuryEarly, mercuryLate, jupiter, descriptionTrap);
        }

        // Six products in one sub-category: five active priced 10 to 50, plus a
        // deactivated one priced next to the cheapest so it would win on
        // proximity if visibility were not honoured.
        public static async Task<VisibilityCatalog> SeedVisibilityCatalogAsync(ApiFixture api)
        {
            using var scope = api.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AgroShopDbContext>();

            var skus = new[] { "VIS-10", "VIS-20", "VIS-30", "VIS-40", "VIS-50", "VIS-HIDDEN" };
            var existing = await LoadBySkusAsync(db, skus);
            if (existing.Count != skus.Length)
            {
                var category = Category.Create("Видимість", "/images/categories/visibility.jpg").Value;
                var subCategory = SubCategory.Create("Перевірка видимості", category.Id).Value;
                var supplier = Supplier.Create("Постачальник видимості", "/images/suppliers/visibility.jpg").Value;

                var products = new List<Product>
                {
                    CreateProduct("Товар десять", "VIS-10", 10m, subCategory.Id, supplier.Id),
                    CreateProduct("Товар двадцять", "VIS-20", 20m, subCategory.Id, supplier.Id),
                    CreateProduct("Товар тридцять", "VIS-30", 30m, subCategory.Id, supplier.Id),
                    CreateProduct("Товар сорок", "VIS-40", 40m, subCategory.Id, supplier.Id),
                    CreateProduct("Товар пятдесят", "VIS-50", 50m, subCategory.Id, supplier.Id),
                    CreateProduct("Товар прихований", "VIS-HIDDEN", 11m, subCategory.Id, supplier.Id),
                };

                // The only way to clear the flag: Create always starts a product
                // active, and Update is what the admin path calls too.
                var hidden = products[^1];
                hidden.Update(
                    hidden.Name.Value,
                    hidden.Description.Value,
                    hidden.Price.Value,
                    hidden.Sku.Value,
                    hidden.StockQuantity.Value,
                    hidden.PackageSize.Amount,
                    hidden.PackageSize.Unit,
                    subCategory.Id,
                    supplier.Id,
                    isActive: false);

                db.Add(category);
                db.Add(subCategory);
                db.Add(supplier);
                db.AddRange(products);
                await db.SaveChangesAsync();

                existing = await LoadBySkusAsync(db, skus);
            }

            var loadedSubCategory = await db.Set<SubCategory>()
                .FirstAsync(sc => sc.Id == existing[0].SubCategoryId);

            return new VisibilityCatalog(
                SubCategory: loadedSubCategory,
                Cheapest: existing[0],
                ActiveNeighbours: existing.Skip(1).Take(4).ToList(),
                Hidden: existing[5],
                MostExpensive: existing[4]);
        }

        private static async Task<List<Product>> LoadBySkusAsync(AgroShopDbContext db, string[] skus)
        {
            var products = await db.Set<Product>()
                .Where(p => skus.Contains(p.Sku.Value))
                .ToListAsync();

            // Callers rely on positional identity, so return in the skus order.
            return products.OrderBy(p => Array.IndexOf(skus, p.Sku.Value)).ToList();
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
