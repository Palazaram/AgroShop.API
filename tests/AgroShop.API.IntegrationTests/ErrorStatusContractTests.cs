using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;

namespace AgroShop.API.IntegrationTests
{
    // Pins the status each shape of failure answers with. The status is the part
    // of a failure a client can act on without parsing the body, so the three
    // shapes have to stay distinguishable: "the resource you addressed is not
    // there" (404), "your payload is wrong" (400), and "that already exists"
    // (409). They were all 400 before, which is why these exist.
    //
    // The error codes are asserted alongside the statuses because a status alone
    // can be right by accident - a 400 from the validation pipeline looks the
    // same as a 400 from the service until you read which error produced it.
    [Collection(ApiCollection.Name)]
    public sealed class ErrorStatusContractTests : IAsyncLifetime
    {
        private readonly ApiFixture _api;
        private CatalogSeeder.SeededCatalog _seeded = null!;

        public ErrorStatusContractTests(ApiFixture api)
        {
            _api = api;
        }

        public async Task InitializeAsync()
        {
            _seeded = await CatalogSeeder.SeedBasicCatalogAsync(_api);
            _api.ClearCache();
        }

        public Task DisposeAsync() => Task.CompletedTask;

        private static async Task<string?> FirstErrorCodeAsync(HttpResponseMessage response)
        {
            using var json = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            return json.RootElement.GetProperty("errors")[0].GetProperty("errorCode").GetString();
        }

        [Fact]
        public async Task Addressing_a_product_that_does_not_exist_answers_404()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/product/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("product.not.found.by.id", await FirstErrorCodeAsync(response));
        }

        // Deliberately a second entity: the point of the change was to make the
        // whole "addressed resource is missing" family answer 404, not to give
        // products a special case their siblings don't share.
        [Fact]
        public async Task Addressing_a_category_that_does_not_exist_answers_404()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync($"/category/{Guid.NewGuid()}");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("category.not.found.by.id", await FirstErrorCodeAsync(response));
        }

        // A route id that isn't a GUID is the caller mistyping an address, not a
        // missing resource - it can't be looked up at all, so it stays a 400.
        [Fact]
        public async Task An_id_that_is_not_a_guid_answers_400()
        {
            var client = _api.CreateClient();

            var response = await client.GetAsync("/product/not-a-guid");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("incorrect.guid.error", await FirstErrorCodeAsync(response));
        }

        // The distinction the split exists for: the endpoint is fine and the
        // resource being created is fine, but a field in the body points at a
        // category that isn't there. Answering 404 would say "/subcategory does
        // not exist", which is false and unactionable for whoever filled a form.
        [Fact]
        public async Task A_payload_referencing_a_category_that_does_not_exist_answers_400()
        {
            var client = _api.CreateClient();

            var response = await client.PostAsJsonAsync(
                "/subcategory",
                new { Name = "Тестова підкатегорія", CategoryId = Guid.NewGuid() });

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal("category.reference.not.found", await FirstErrorCodeAsync(response));
        }

        [Fact]
        public async Task Creating_a_sub_category_whose_name_is_taken_answers_409()
        {
            var client = _api.CreateClient();
            var payload = new { Name = "Підкатегорія конфлікту", CategoryId = _seeded.Category.Id };

            var first = await client.PostAsJsonAsync("/subcategory", payload);
            Assert.Equal(HttpStatusCode.OK, first.StatusCode);

            var duplicate = await client.PostAsJsonAsync("/subcategory", payload);

            Assert.Equal(HttpStatusCode.Conflict, duplicate.StatusCode);
            Assert.Equal(
                "sub.category.name.already.exists.in.category",
                await FirstErrorCodeAsync(duplicate));
        }

        [Fact]
        public async Task Creating_a_product_whose_sku_is_taken_answers_409()
        {
            var client = _api.CreateClient();

            using var form = new MultipartFormDataContent
            {
                { new StringContent("Насіння тестове"), "Name" },
                { new StringContent("Опис товару для перевірки унікальності артикулу."), "Description" },
                { new StringContent("10"), "Price" },
                // The clash: a code one of the seeded products already carries.
                { new StringContent(_seeded.Products[0].Sku.Value), "Sku" },
                { new StringContent("5"), "StockQuantity" },
                { new StringContent("1"), "PackageAmount" },
                { new StringContent("Gram"), "PackageUnit" },
                { new StringContent(_seeded.SubCategory.Id.ToString()), "SubCategoryId" },
                { new StringContent(_seeded.Supplier.Id.ToString()), "SupplierId" },
            };

            // The image only has to get past validation, which inspects the
            // extension and the length - not the bytes - so these three will do.
            var image = new ByteArrayContent([1, 2, 3]);
            image.Headers.ContentType = new MediaTypeHeaderValue("image/png");
            form.Add(image, "Image", "x.png");

            var response = await client.PostAsync("/product", form);

            Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
            Assert.Equal("product.sku.already.exists", await FirstErrorCodeAsync(response));
        }

        [Fact]
        public async Task Linking_an_attribute_to_a_sub_category_twice_answers_409()
        {
            var client = _api.CreateClient();

            // Its own subcategory, not the seeded one: linking an attribute to a
            // subcategory makes that attribute mandatory for products created in
            // it, which would break the SKU fact above. xUnit does not promise an
            // order for facts in a class, so the isolation has to be structural.
            var subCategoryId = await CreateAndFindIdAsync(
                client, "/subcategory", "Підкатегорія звязку",
                new { Name = "Підкатегорія звязку", CategoryId = _seeded.Category.Id });

            var attributeId = await CreateAndFindIdAsync(
                client, "/attribute", "Тестовий атрибут",
                new { Name = "Тестовий атрибут", ValueType = "SingleSelect" });

            var payload = new { SubCategoryId = subCategoryId, AttributeId = attributeId };

            var first = await client.PostAsJsonAsync("/productattribute", payload);
            Assert.Equal(HttpStatusCode.OK, first.StatusCode);

            var duplicate = await client.PostAsJsonAsync("/productattribute", payload);

            Assert.Equal(HttpStatusCode.Conflict, duplicate.StatusCode);
            Assert.Equal(
                "product.attribute.already.exists.for.sub.category",
                await FirstErrorCodeAsync(duplicate));
        }

        // The create endpoints answer with an empty envelope rather than the new
        // row, so the id has to be read back from the collection by name.
        private static async Task<Guid> CreateAndFindIdAsync(
            HttpClient client, string route, string name, object payload)
        {
            var created = await client.PostAsJsonAsync(route, payload);
            Assert.Equal(HttpStatusCode.OK, created.StatusCode);

            var listed = await client.GetAsync(route);
            Assert.Equal(HttpStatusCode.OK, listed.StatusCode);

            using var json = JsonDocument.Parse(await listed.Content.ReadAsStringAsync());
            var match = json.RootElement.GetProperty("result")
                .EnumerateArray()
                .Single(item => item.GetProperty("name").GetString() == name);

            return match.GetProperty("id").GetGuid();
        }
    }
}
