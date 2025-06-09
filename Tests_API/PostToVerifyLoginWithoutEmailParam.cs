using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class PostToVerifyLoginWithoutEmailParam : TestFixtureBase
    {
        [Test]
        public async Task Post_To_Verify_Login_Without_Email_Param()
        {
            /*
            API URL: https://automationexercise.com/api/verifyLogin
            Request Method: POST
            Request Parameter: password
            Response Code: 400
            Response Message: Bad request, email or password parameter is missing in POST request.
            */

            var formData = Context.APIRequest.CreateFormData();

            //requst body with empty string as email
            //formData.Set("email", "");
            formData.Set("password", Configuration["ValidCredentials:Password"]);

            //pass the login details to the post request
            var response = await ApiContext.PostAsync($"{baseUrl}/api/verifyLogin", new APIRequestContextOptions
            {
                Form = formData
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

            //assert 400 response and bad request
            Assert.That(responseCode, Is.EqualTo(400));
            Assert.That(message, Does.Contain("bad request, email or password parameter is missing").IgnoreCase);
        }
    }
}
