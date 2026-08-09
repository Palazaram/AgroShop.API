using System.Net;
using System.Text.Json;
using Xunit;

namespace AgroShop.API.IntegrationTests
{
    // IsActive existed on the entity and in the DTO but changed nothing: a
    // deactivated product still appeared in listings, was counted in facets and
    // opened by direct link. These pin the flag's meaning on every public read,
    // because a half-applied rule is worse than none - a shopper would see a
    // product in the catalog and a 404 when opening it, or the reverse.
    [Collection(ApiCollection.Name)]
    public sealed class ProductVisibilityTests : IAsyncLifetime
    {
        private readonly ApiFixture _api;
        private CatalogSeeder.VisibilityCatalog _seeded = null!;

        public ProductVisibilityTests(ApiFixture api)
        {
            _api = api;
        }

        public async Task InitializeAsync()
        {
            _seeded = await CatalogSeeder.SeedVisibilityCatalogAsync(_api);
            _api.ClearCache();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private string SubCategoryScope => $"subCategoryIds={_seeded.SubCategory.Id}";

        private static async Task<JsonElement> ResultOfAsync(HttpResponseMessage response)
        {
            var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return json.RootElement.GetProperty("result").Clone();
        }

        [Fact]
        public async Task A_deactivated_product_is_absent_from_the_listing()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/product?{SubCategoryScope}&pageSize=100");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await ResultOfAsync(response);

            var returnedSkus = result.GetProperty("items")
                .EnumerateArray()
                .Select(item => item.GetProperty("sku").GetString())
                .ToList();

            Assert.DoesNotContain(_seeded.Hidden.Sku.Value, returnedSkus);
            Assert.Contains(_seeded.Cheapest.Sku.Value, returnedSkus);
            // The sub-category holds six products, one of them deactivated.
            Assert.Equal(5, result.GetProperty("totalCount").GetInt32());
        }

        // Facets drive the filter sidebar's counts. If they counted hidden
        // products the sidebar would promise results the listing then wouldn't
        // deliver - the one inconsistency a shopper actually notices.
        [Fact]
        public async Task A_deactivated_product_is_absent_from_the_facet_counts()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/product/facets?{SubCategoryScope}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await ResultOfAsync(response);

            Assert.Equal(5, result.GetProperty("totalCount").GetInt32());
        }

        [Fact]
        public async Task A_deactivated_product_answers_404_by_direct_link()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/product/{_seeded.Hidden.Id}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task An_active_product_still_opens_by_direct_link()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/product/{_seeded.Cheapest.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await ResultOfAsync(response);
            Assert.Equal(_seeded.Cheapest.Sku.Value, result.GetProperty("sku").GetString());
        }
    }
}
