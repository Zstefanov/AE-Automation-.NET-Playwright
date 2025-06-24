using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class API_Login : TestFixtureBase
    {
        [Test]
        public async Task Login_API()
        {
            /*
         * API URL: https://automationexercise.com/api/verifyLogin
         * Request Method: POST
         * Required parameters:
         *   - email (form parameter)
         *   - password (form parameter)
         * Expected Response Code: 200
         * Response Message: User exists!
         */

            var email = Configuration["ValidCredentials:EmailAddress"];
            var password = Configuration["ValidCredentials:Password"];

            var formData = Context.APIRequest.CreateFormData();
            formData.Set("email", email);
            formData.Set("password", password);

            var response = await ApiContext.PostAsync($"{baseUrl}/api/verifyLogin", new APIRequestContextOptions
            {
                Form = formData
            });

            var responseBody = await response.TextAsync();

            TestContext.WriteLine($"Actual Status Code: {response.Status}");
            TestContext.WriteLine("Response Body: " + responseBody);

            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;
            var responseCode = root.GetProperty("responseCode").GetInt32();
            var message = root.GetProperty("message").GetString();

            Assert.That(responseCode, Is.EqualTo(200));
            Assert.That(message, Does.Contain("User exists!").IgnoreCase);

        }
    }
}
