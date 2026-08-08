using System.Net;
using System.Text.Json;
using Xunit;

namespace AgroShop.API.IntegrationTests
{
    // The search semantics agreed in the spec (Palazaram/agro-shop#2),
    // asserted at the HTTP seam. Queries use name tokens unique to the
    // search catalog ("меркурій", "юпітер", "огірок") so exact-count
    // assertions hold in the collection-shared database.
    [Collection(ApiCollection.Name)]
    public sealed class ProductSearchTests : IAsyncLifetime
    {
        private readonly ApiFixture _api;
        private CatalogSeeder.SearchCatalog _catalog = null!;

        public ProductSearchTests(ApiFixture api)
        {
            _api = api;
        }

        public async Task InitializeAsync()
        {
            _catalog = await CatalogSeeder.SeedSearchCatalogAsync(_api);
            _api.ClearCache();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        [Fact]
        public async Task Multi_word_query_matches_names_containing_all_words_in_any_order()
        {
            var first = await SearchAsync("огірок меркурій");
            var second = await SearchAsync("меркурій огірок");

            foreach (var listing in new[] { first, second })
            {
                Assert.Equal(2, listing.TotalCount);
                Assert.Contains(_catalog.MercuryEarly.Name.Value, listing.Names);
                Assert.Contains(_catalog.MercuryLate.Name.Value, listing.Names);
            }
        }

        [Fact]
        public async Task Whole_query_matches_sku_as_substring()
        {
            var listing = await SearchAsync("CUC-JUP");

            Assert.Equal(1, listing.TotalCount);
            Assert.Contains(_catalog.Jupiter.Name.Value, listing.Names);
        }

        [Fact]
        public async Task Word_splitting_does_not_apply_to_the_sku()
        {
            // Split words would each match "CUC-JUP-01"; the whole query
            // (with its space) must not.
            var listing = await SearchAsync("CUC JUP");

            Assert.Equal(0, listing.TotalCount);
        }

        [Fact]
        public async Task Matching_is_case_insensitive_for_cyrillic_and_latin()
        {
            var cyrillic = await SearchAsync("МЕРКУРІЙ ОГІРОК");
            Assert.Equal(2, cyrillic.TotalCount);

            var latin = await SearchAsync("cuc-jup");
            Assert.Equal(1, latin.TotalCount);
            Assert.Contains(_catalog.Jupiter.Name.Value, latin.Names);
        }

        [Fact]
        public async Task Queries_under_two_characters_behave_as_no_search()
        {
            var baseline = await SearchAsync(null);
            var oneChar = await SearchAsync(" м ");

            Assert.Equal(baseline.TotalCount, oneChar.TotalCount);
        }

        [Fact]
        public async Task Description_is_not_matched()
        {
            // The trap product carries "меркурій" only in its description.
            var listing = await SearchAsync("меркурій");

            Assert.Equal(2, listing.TotalCount);
            Assert.DoesNotContain(_catalog.DescriptionTrap.Name.Value, listing.Names);
        }

        [Fact]
        public async Task Search_composes_with_filters_sorting_and_pagination()
        {
            // Narrowed by supplier: only the zucchini supplier's cucumber remains.
            var filtered = await SearchAsync("огірок", $"&supplierIds={_catalog.ZucchiniSupplier.Id}");
            Assert.Equal(1, filtered.TotalCount);
            Assert.Contains(_catalog.Jupiter.Name.Value, filtered.Names);

            // Sorted by ascending price and paged by 2: page 2 holds only the
            // most expensive of the three cucumbers.
            var paged = await SearchAsync("огірок", "&sortBy=priceAsc&pageSize=2&page=2");
            Assert.Equal(3, paged.TotalCount);
            Assert.Equal(2, paged.TotalPages);
            Assert.Equal(new[] { _catalog.Jupiter.Name.Value }, paged.Names);
        }

        private sealed record Listing(int TotalCount, int TotalPages, List<string?> Names);

        private async Task<Listing> SearchAsync(string? search, string extraQuery = "")
        {
            var client = _api.CreateClient();
            var url = search == null
                ? $"/product?pageSize=100{extraQuery}"
                : $"/product?search={Uri.EscapeDataString(search)}{(extraQuery.Length > 0 ? extraQuery : "&pageSize=100")}";

            var response = await client.GetAsync(url);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var result = json.RootElement.GetProperty("result");

            return new Listing(
                result.GetProperty("totalCount").GetInt32(),
                result.GetProperty("totalPages").GetInt32(),
                result.GetProperty("items").EnumerateArray()
                    .Select(item => item.GetProperty("name").GetString())
                    .ToList());
        }
    }
}
