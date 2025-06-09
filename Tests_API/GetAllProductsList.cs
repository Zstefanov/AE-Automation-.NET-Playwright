using AE_extensive_project.Models;
using AE_extensive_project.TestFixture;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class GetAllProductsList : TestFixtureBase
    {
        [Test]
        public async Task Get_All_Products_List()
        {
            /*
             * API 1: Get All Products List
                API URL: https://automationexercise.com/api/productsList
                Request Method: GET
                Response Code: 200
                Response JSON: All products list
             */

            var response = await ApiContext.GetAsync($"{baseUrl}/api/productsList");

            //assert OK response from endpoint
            Assert.That(response.Status, Is.EqualTo(200));

            //get responsebody so we can deserialize and extract products
            var responseBody = await response.TextAsync();

            var productList = JsonSerializer.Deserialize<ProductResponse>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.That(productList, Is.Not.Null);
            Assert.That(productList.Products, Is.Not.Null.And.Not.Empty);

            //provide list of products(models) in output
            TestContext.WriteLine($"Total Products: {productList.Products.Count}");

            foreach (var product in productList.Products)
            {
                TestContext.WriteLine($"Product ID: {product.Id}, Name: {product.Name}, Price: {product.Price}");
            }
        }
    }
}
