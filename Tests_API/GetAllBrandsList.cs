using AE_extensive_project.TestFixture;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class GetAllBrandsList : TestFixtureBase
    {
        [Test]
        public async Task Get_All_Brands_LisT()
        {
            /*
             * API URL: https://automationexercise.com/api/brandsList
                Request Method: GET
                Response Code: 200
                Response JSON: All brands list
             */

            var response = await ApiContext.GetAsync($"{baseUrl}/api/brandsList");
            Assert.That(response.Status, Is.EqualTo(200));

            var responseBody = await response.TextAsync();

            //parse the response so we can get response code and brands
            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            var responseCode = root.GetProperty("responseCode").GetInt32();
            var brands = root.GetProperty("brands");

            Assert.That(responseCode, Is.EqualTo(200));
            Assert.That(brands.GetArrayLength(), Is.GreaterThan(0));

            //print all items
            TestContext.WriteLine($"Total Brands: {brands.GetArrayLength()}");
            foreach (var brand in brands.EnumerateArray())
            {
                var id = brand.GetProperty("id").GetInt32();
                var name = brand.GetProperty("brand").GetString();
                TestContext.WriteLine($"Brand ID: {id}, Name: {name}");
            }
        }
    }
}
