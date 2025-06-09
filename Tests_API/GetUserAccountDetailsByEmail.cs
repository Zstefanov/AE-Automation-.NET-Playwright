using AE_extensive_project.TestFixture;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class GetUserAccountDetailsByEmail : TestFixtureBase
    {
        [Test]
        public async Task Get_User_Account_Details_By_Email()
        {
            /*
             * API URL: https://automationexercise.com/api/getUserDetailByEmail
                Request Method: GET
                Request Parameters: email
                Response Code: 200
                Response JSON: User Detail
             */

            var email = Configuration["ValidCredentials:EmailAddress"];
            //uri.escapedatastring for special chars in email(just in case)
            var url = $"{baseUrl}/api/getUserDetailByEmail?email={Uri.EscapeDataString(email)}";

            var response = await ApiContext.GetAsync(url);
            var responseBody = await response.TextAsync();

            //log status for debugging
            TestContext.WriteLine($"Actual Status Code: {response.Status}");
            TestContext.WriteLine("Response Body: " + responseBody);

            //parse the json object we receive as a response to extract code
            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;
            var responseCode = root.GetProperty("responseCode").GetInt32();

            //iteration over the root element properties for clarity
            foreach (var property in root.EnumerateObject())
            {
                TestContext.WriteLine($"{property.Name}: {property.Value}");
            }

            //extract the user details if available and validate email address
            if (root.TryGetProperty("user", out var userDetails))
            {
                var name = userDetails.GetProperty("name").GetString();
                var emailResp = userDetails.GetProperty("email").GetString();
                var country = userDetails.GetProperty("country").GetString();

                TestContext.WriteLine($"Extracted Name: {name}");
                TestContext.WriteLine($"Extracted Email: {emailResp}");
                TestContext.WriteLine($"Extracted Country: {country}");

                Assert.That(emailResp, Is.EqualTo(email));
            }

            //assert 200 response
            Assert.That(responseCode, Is.EqualTo(200));
        }
    }
}
