using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class DeleteToVerifyLogin : TestFixtureBase
    {
        [Test]
        public async Task Delete_To_Verify_Login()
        {
            /*
             * API URL: https://automationexercise.com/api/verifyLogin
                Request Method: DELETE
                Response Code: 405
                Response Message: This request method is not supported.
             */

            var formData = Context.APIRequest.CreateFormData();

            //requst body with empty string as email
            formData.Set("email", Configuration["ValidCredentials:EmailAddress"]);
            formData.Set("password", Configuration["ValidCredentials:Password"]);

            //pass the login details with DELETE request
            var response = await ApiContext.DeleteAsync($"{baseUrl}/api/verifyLogin", new APIRequestContextOptions
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

            //assert 405 response request method not supported
            Assert.That(responseCode, Is.EqualTo(405));
            Assert.That(message, Does.Contain("this request method is not supported").IgnoreCase);
        }
    }
}
