using AE_extensive_project.TestFixture;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class PostSeachProductWithoutParameter : TestFixtureBase
    {
        [Test]
        public async Task Post_Seach_Product_Without_Parameter()
        {
            /*
             * API URL: https://automationexercise.com/api/searchProduct
                Request Method: POST
                Response Code: 400
                Response Message: Bad request, search_product parameter is missing in POST request.
             */

            //send empty body request
            var response = await ApiContext.PostAsync("https://automationexercise.com/api/searchProduct");

            var responseBody = await response.TextAsync();

            //log status for debugging
            TestContext.WriteLine($"Actual Status Code: {response.Status}");
            TestContext.WriteLine("Response Body: " + responseBody);

            //parse the json object we receive as a response to extract code
            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;
            var responseCode = root.GetProperty("responseCode").GetInt32();
            var message = root.GetProperty("message").GetString();

            //assert 400 response and search param missing
            Assert.That(responseCode, Is.EqualTo(400));
            Assert.That(message, Does.Contain("search_product parameter is missing").IgnoreCase);
        }
    }
}
