using AE_extensive_project.TestFixture;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class PostToSearchProduct : TestFixtureBase
    {
        [Test]
        public async Task Post_To_Search_Product()
        {
            /*
             * API URL: https://automationexercise.com/api/searchProduct
                Request Method: POST
                Request Parameter: search_product (For example: top, tshirt, jean)
                Response Code: 200
                Response JSON: Searched products list
             */

            var formData = Context.APIRequest.CreateFormData();
            formData.Set("search_product", "");

            var response = await ApiContext.PostAsync($"{baseUrl}/api/searchProduct", new()
            {
                Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/x-www-form-urlencoded" }
            },
                Form = formData
            });

            Assert.That(response.Status, Is.EqualTo(200));

            var responseBody = await response.TextAsync();
            TestContext.WriteLine("Raw Response:");
            TestContext.WriteLine(responseBody);

            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            var responseCode = root.GetProperty("responseCode").GetInt32();
            var products = root.GetProperty("products");

            Assert.That(responseCode, Is.EqualTo(200));
            Assert.That(products.GetArrayLength(), Is.GreaterThan(0));

            TestContext.WriteLine($"Total Products Found: {products.GetArrayLength()}");

            foreach (var product in products.EnumerateArray())
            {
                var id = product.GetProperty("id").GetInt32();
                var name = product.GetProperty("name").GetString();
                var price = product.GetProperty("price").GetString();
                TestContext.WriteLine($"Product ID: {id}, Name: {name}, Price: {price}");
            }
        }

        [Test]
        [TestCase("top")]
        [TestCase("tshirt")]
        [TestCase("jean")]
        public async Task SearchProduct_WithValidSearchTerm_ReturnsProductsList(string searchTerm)
        {
            //arrange - Use FormData collection for Playwright
            var formData = Context.APIRequest.CreateFormData();
            formData.Set("search_product", searchTerm);

            //act
            var response = await ApiContext.PostAsync($"{baseUrl}/api/searchProduct", new()
            {
                Form = formData
            });

            //assert
            Assert.That(response.Status, Is.EqualTo(200),
                $"Expected status code 200, but got {response.Status}");

            var responseBody = await response.TextAsync();
            Assert.That(responseBody, Is.Not.Null.And.Not.Empty,
                "Response body should not be null or empty");

            //verify response is valid JSON
            JsonDocument jsonDoc = null;
            Assert.DoesNotThrow(() => jsonDoc = JsonDocument.Parse(responseBody),
                "Response should be valid JSON");

            using (jsonDoc)
            {
                //verify response contains products array
                Assert.That(jsonDoc.RootElement.TryGetProperty("products", out var productsElement),
                    Is.True, "Response should contain 'products' property");

                Assert.That(productsElement.ValueKind, Is.EqualTo(JsonValueKind.Array),
                    "Products should be an array");

                //log response for debugging
                TestContext.WriteLine($"Search term: {searchTerm}");
                TestContext.WriteLine($"Response: {responseBody}");
                TestContext.WriteLine($"Products count: {productsElement.GetArrayLength()}");
            }
        }
        [Test]
        public async Task SearchProduct_WithEmptySearchTerm_ReturnsResponse()
        {
            //arrange
            var formData = Context.APIRequest.CreateFormData();
            formData.Set("search_product", "");

            //act
            var response = await ApiContext.PostAsync($"{baseUrl}/api/searchProduct", new()
            {
                Form = formData
            });

            //assert
            Assert.That(response.Status, Is.EqualTo(200));

            var responseBody = await response.TextAsync();
            Assert.That(responseBody, Is.Not.Null.And.Not.Empty);

            //verify it's valid JSON
            Assert.DoesNotThrow(() => JsonDocument.Parse(responseBody));

            TestContext.WriteLine($"Empty search response: {responseBody}");
        }
    }
}
