using System.Net;
using System.Text.Json;
using Xunit;

namespace AgroShop.API.IntegrationTests
{
    // Search is a SCOPE for the facets, not another facet: it applies to
    // every dimension, the option universe and totalCount alike, and is
    // never self-excluded. Options with no representation inside the scope
    // disappear from the response entirely - unlike ticking a facet, which
    // keeps siblings listed at zero.
    //
    // Both seed catalogs are involved on purpose: the tomato catalog is the
    // "rest of the world" that a search for cucumbers must scope away.
    [Collection(ApiCollection.Name)]
    public sealed class ProductFacetsSearchTests : IAsyncLifetime
    {
        private readonly ApiFixture _api;
        private CatalogSeeder.SearchCatalog _catalog = null!;
        private CatalogSeeder.SeededCatalog _restOfWorld = null!;

        public ProductFacetsSearchTests(ApiFixture api)
        {
            _api = api;
        }

        public async Task InitializeAsync()
        {
            _catalog = await CatalogSeeder.SeedSearchCatalogAsync(_api);
            _restOfWorld = await CatalogSeeder.SeedBasicCatalogAsync(_api);
            _api.ClearCache();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Facets_totalCount_and_counts_describe_the_search_scope()
        {
            var facets = await GetFacetsAsync("search=" + Uri.EscapeDataString("огірок"));

            Assert.Equal(3, facets.RootElement.GetProperty("result").GetProperty("totalCount").GetInt32());

            var suppliers = SupplierCounts(facets);
            Assert.Equal(2, suppliers[_catalog.CucumberSupplier.Id]);
            Assert.Equal(1, suppliers[_catalog.ZucchiniSupplier.Id]);
        }

        [Fact]
        public async Task Options_absent_from_the_search_results_are_absent_from_the_response()
        {
            var facets = await GetFacetsAsync("search=" + Uri.EscapeDataString("огірок"));

            var suppliers = SupplierCounts(facets);
            Assert.DoesNotContain(_restOfWorld.Supplier.Id, suppliers.Keys);

            var subCategoryIds = facets.RootElement.GetProperty("result").GetProperty("subCategoryOptions")
                .EnumerateArray()
                .Select(o => o.GetProperty("subCategoryId").GetGuid())
                .ToList();
            Assert.DoesNotContain(_restOfWorld.SubCategory.Id, subCategoryIds);
        }

        [Fact]
        public async Task Search_combines_with_a_ticked_facet_and_self_exclusion_still_applies()
        {
            var facets = await GetFacetsAsync(
                "search=" + Uri.EscapeDataString("огірок") + $"&supplierIds={_catalog.ZucchiniSupplier.Id}");

            var result = facets.RootElement.GetProperty("result");

            // totalCount takes the filters exactly as passed: scope + tick.
            Assert.Equal(1, result.GetProperty("totalCount").GetInt32());

            // The supplier facet self-excludes its own dimension but keeps
            // the scope: both in-scope suppliers stay listed with the counts
            // they'd contribute inside the search.
            var suppliers = SupplierCounts(facets);
            Assert.Equal(2, suppliers[_catalog.CucumberSupplier.Id]);
            Assert.Equal(1, suppliers[_catalog.ZucchiniSupplier.Id]);

            // A neighbouring facet respects both the scope and the tick: only
            // the zucchini supplier's single cucumber remains behind 5 г.
            var fiveGram = result.GetProperty("packageOptions").EnumerateArray()
                .Single(o => o.GetProperty("packageUnit").GetString() == "Gram"
                          && o.GetProperty("packageAmount").GetDecimal() == 5m);
            Assert.Equal(1, fiveGram.GetProperty("productCount").GetInt32());
        }

        [Fact]
        public async Task Short_queries_behave_as_no_search_scope()
        {
            var baseline = await GetFacetsAsync(null);
            var oneChar = await GetFacetsAsync("search=" + Uri.EscapeDataString(" м "));

            Assert.Equal(
                baseline.RootElement.GetProperty("result").GetProperty("totalCount").GetInt32(),
                oneChar.RootElement.GetProperty("result").GetProperty("totalCount").GetInt32());
        }

        private async Task<JsonDocument> GetFacetsAsync(string? query)
        {
            var client = _api.CreateClient();
            var response = await client.GetAsync(query == null ? "/product/facets" : $"/product/facets?{query}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            return JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        }

        private static Dictionary<Guid, int> SupplierCounts(JsonDocument facets) =>
            facets.RootElement.GetProperty("result").GetProperty("supplierOptions")
                .EnumerateArray()
                .ToDictionary(
                    o => o.GetProperty("supplierId").GetGuid(),
                    o => o.GetProperty("productCount").GetInt32());
    }
}
