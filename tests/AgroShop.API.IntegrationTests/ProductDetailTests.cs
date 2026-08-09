using System.Net;
using System.Text.Json;
using Xunit;

namespace AgroShop.API.IntegrationTests
{
    // The single-product response is what the product page is built from, so
    // the fields it carries are a contract rather than an implementation
    // detail. This pins the parent category in particular: it exists purely so
    // a client can render a trail down to the product, and nothing inside the
    // API would notice if it silently stopped being populated.
    [Collection(ApiCollection.Name)]
    public sealed class ProductDetailTests : IAsyncLifetime
    {
        private readonly ApiFixture _api;
        private CatalogSeeder.SeededCatalog _seeded = null!;

        public ProductDetailTests(ApiFixture api)
        {
            _api = api;
        }

        public async Task InitializeAsync()
        {
            _seeded = await CatalogSeeder.SeedBasicCatalogAsync(_api);
            _api.ClearCache();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Product_detail_carries_the_whole_path_from_category_to_product()
        {
            var client = _api.CreateClient();
            var product = _seeded.Products[0];

            var response = await client.GetAsync($"/product/{product.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var result = json.RootElement.GetProperty("result");

            Assert.Equal(_seeded.Category.Id, result.GetProperty("categoryId").GetGuid());
            Assert.Equal(_seeded.Category.Name.Value, result.GetProperty("categoryName").GetString());

            // Asserted alongside so a regression that swapped the two levels
            // over - a plausible mistake, since one is reached through the
            // other - can't pass by filling both from the sub-category.
            Assert.Equal(_seeded.SubCategory.Id, result.GetProperty("subCategoryId").GetGuid());
            Assert.Equal(
                _seeded.SubCategory.Name.Value,
                result.GetProperty("subCategoryName").GetString());
        }

        // The mapper is shared with the listing endpoint, where the category is
        // reached through a different query. Populating it only for the detail
        // path would be an easy half-fix, and a null navigation there would
        // throw rather than return a partial DTO.
        [Fact]
        public async Task Product_listing_carries_the_parent_category_too()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync("/product?pageSize=100");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var items = json.RootElement.GetProperty("result").GetProperty("items");

            var seededItem = items
                .EnumerateArray()
                .Single(item => item.GetProperty("id").GetGuid() == _seeded.Products[0].Id);

            Assert.Equal(_seeded.Category.Id, seededItem.GetProperty("categoryId").GetGuid());
            Assert.Equal(
                _seeded.Category.Name.Value,
                seededItem.GetProperty("categoryName").GetString());
        }
    }
}
