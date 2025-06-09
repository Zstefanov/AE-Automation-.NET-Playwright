using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class PostToVerifyLoginWithValidDetails : TestFixtureBase
    {
        [Test]
        public async Task Post_To_Verify_Login_With_Valid_Details()
        {
            /*
            *API URL: https://automationexercise.com/api/verifyLogin
            Request Method: POST
            Request Parameters: email, password
            Response Code: 200
            Response Message: User exists!
            */

            var formData = Context.APIRequest.CreateFormData();

            //valid login details from appsettings file
            formData.Set("email", Configuration["ValidCredentials:EmailAddress"]);
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

            //assert 200 response and the user exists
            Assert.That(responseCode, Is.EqualTo(200));
            Assert.That(message, Does.Contain("user exists").IgnoreCase);
        }
    }
}
