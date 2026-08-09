using System.Net;
using System.Text.Json;
using Xunit;

namespace AgroShop.API.IntegrationTests
{
    // The product page's "similar products" row used to be the listing endpoint
    // asked for one page of five, which works only while a sub-category fits in
    // that page. Past it, every product outside the first page showed the same
    // row and the client-side self-exclusion never fired, because the product
    // being viewed wasn't in the response at all.
    //
    // These pin the behaviour that replaces it: the server picks neighbours by
    // price proximity to the product being viewed, so the answer differs per
    // product and excludes the product itself by construction.
    [Collection(ApiCollection.Name)]
    public sealed class SimilarProductsTests : IAsyncLifetime
    {
        private readonly ApiFixture _api;
        private CatalogSeeder.VisibilityCatalog _seeded = null!;

        public SimilarProductsTests(ApiFixture api)
        {
            _api = api;
        }

        public async Task InitializeAsync()
        {
            _seeded = await CatalogSeeder.SeedVisibilityCatalogAsync(_api);
            _api.ClearCache();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private static async Task<List<string?>> SkusOfAsync(HttpResponseMessage response)
        {
            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return json.RootElement.GetProperty("result")
                .EnumerateArray()
                .Select(item => item.GetProperty("sku").GetString())
                .ToList();
        }

        // The cheapest product is the case the old approach got wrong: taking
        // the first page by price descending would answer with the four most
        // expensive, the same four every cheap product would see.
        [Fact]
        public async Task Neighbours_are_the_closest_in_price_not_the_most_expensive()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/product/{_seeded.Cheapest.Id}/similar?take=4");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var skus = await SkusOfAsync(response);

            // Priced 10; the active neighbours are 20, 30, 40 and 50.
            Assert.Equal(new[] { "VIS-20", "VIS-30", "VIS-40", "VIS-50" }, skus);
        }

        [Fact]
        public async Task The_product_being_viewed_is_never_among_its_own_neighbours()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/product/{_seeded.MostExpensive.Id}/similar?take=4");

            var skus = await SkusOfAsync(response);

            Assert.DoesNotContain(_seeded.MostExpensive.Sku.Value, skus);
            Assert.Equal(4, skus.Count);
        }

        // Priced 11, next to the cheapest, so it would come first on proximity
        // if visibility weren't honoured here as well.
        [Fact]
        public async Task Deactivated_products_are_not_offered_as_neighbours()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/product/{_seeded.Cheapest.Id}/similar?take=4");

            Assert.DoesNotContain("VIS-HIDDEN", await SkusOfAsync(response));
        }

        [Fact]
        public async Task Asking_for_neighbours_of_a_product_that_does_not_exist_answers_404()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/product/{Guid.NewGuid()}/similar");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

            // The error code, not just the status: routing answers 404 for an
            // address that doesn't exist at all, so without this the fact would
            // pass just as happily before the endpoint was written.
            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            Assert.Equal(
                "product.not.found.by.id",
                json.RootElement.GetProperty("errors")[0].GetProperty("errorCode").GetString());
        }
    }
}
