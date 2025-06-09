using AE_extensive_project.TestFixture;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class PostToAllProductsList : TestFixtureBase
    {
        [Test]
        public async Task Post_To_All_Products_List()
        {
            /*
             * API URL: https://automationexercise.com/api/productsList
                Request Method: POST
                Response Code: 405
                Response Message: This request method is not supported.
             */

            var response = await ApiContext.PostAsync($"{baseUrl}/api/productsList", new()
            {
                Headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            },
                DataObject = new { test = "value" } // оtional: sends dummy body
            });

            var responseBody = await response.TextAsync();
            
            //log status for debugging
            TestContext.WriteLine($"Actual Status Code: {response.Status}");
            TestContext.WriteLine("Response Body: " + responseBody);

            //parse the json object we receive as a response to extract code
            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;
            var responseCode = root.GetProperty("responseCode").GetInt32();
            var message = root.GetProperty("message").GetString();

            //assert 405 response and not supported
            Assert.That(responseCode, Is.EqualTo(405));
            Assert.That(message, Does.Contain("not supported").IgnoreCase);
        }
    }
}
