using System.Net;
using System.Text.Json;
using Xunit;

namespace AgroShop.API.IntegrationTests
{
    [Collection(ApiCollection.Name)]
    public sealed class ProductListingTests
    {
        private readonly ApiFixture _api;

        public ProductListingTests(ApiFixture api)
        {
            _api = api;
        }

        [Fact]
        public async Task Product_listing_returns_seeded_products_with_paging_metadata()
        {
            var seeded = await CatalogSeeder.SeedBasicCatalogAsync(_api);
            var client = _api.CreateClient();

            var response = await client.GetAsync("/product");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var result = json.RootElement.GetProperty("result");

            Assert.Equal(seeded.Products.Count, result.GetProperty("totalCount").GetInt32());
            Assert.Equal(1, result.GetProperty("page").GetInt32());
            Assert.Equal(20, result.GetProperty("pageSize").GetInt32());
            Assert.Equal(1, result.GetProperty("totalPages").GetInt32());

            var returnedNames = result.GetProperty("items")
                .EnumerateArray()
                .Select(item => item.GetProperty("name").GetString())
                .ToList();

            Assert.Equal(seeded.Products.Count, returnedNames.Count);
            foreach (var product in seeded.Products)
            {
                Assert.Contains(product.Name.Value, returnedNames);
            }
        }
    }
}
