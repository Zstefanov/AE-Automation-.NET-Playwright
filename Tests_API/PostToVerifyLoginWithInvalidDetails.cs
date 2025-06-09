using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class PostToVerifyLoginWithInvalidDetails : TestFixtureBase
    {
        [Test]
        public async Task Post_To_Verify_Login_With_Invalid_Details()
        {
            /*
             * API URL: https://automationexercise.com/api/verifyLogin
                Request Method: POST
                Request Parameters: email, password (invalid values)
                Response Code: 404
                Response Message: User not found!
             */

            var formData = Context.APIRequest.CreateFormData();

            //invalid login details from appsettings file
            formData.Set("email", Configuration["InvalidCredentials:EmailAddress"]);
            formData.Set("password", Configuration["InvalidCredentials:Password"]);

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

            //assert 404 response and user not found message
            Assert.That(responseCode, Is.EqualTo(404));
            Assert.That(message, Does.Contain("user not found").IgnoreCase);
        }
    }
}
