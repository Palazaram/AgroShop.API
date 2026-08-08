using System.Net;
using System.Text.Json;
using Xunit;

namespace AgroShop.API.IntegrationTests
{
    [Collection(ApiCollection.Name)]
    public sealed class ProductListingTests : IAsyncLifetime
    {
        private readonly ApiFixture _api;
        private CatalogSeeder.SeededCatalog _seeded = null!;

        public ProductListingTests(ApiFixture api)
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
        public async Task Product_listing_returns_seeded_products_with_paging_metadata()
        {
            var client = _api.CreateClient();

            // pageSize=100 (the backend maximum) so the seeded products can't
            // fall off the page as other test classes grow the shared database.
            var response = await client.GetAsync("/product?pageSize=100");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var result = json.RootElement.GetProperty("result");

            var totalCount = result.GetProperty("totalCount").GetInt32();
            Assert.True(totalCount >= _seeded.Products.Count);
            Assert.Equal(1, result.GetProperty("page").GetInt32());
            Assert.Equal(100, result.GetProperty("pageSize").GetInt32());
            Assert.Equal((int)Math.Ceiling(totalCount / 100.0), result.GetProperty("totalPages").GetInt32());

            var returnedNames = result.GetProperty("items")
                .EnumerateArray()
                .Select(item => item.GetProperty("name").GetString())
                .ToList();

            foreach (var product in _seeded.Products)
            {
                Assert.Contains(product.Name.Value, returnedNames);
            }
        }
    }
}
