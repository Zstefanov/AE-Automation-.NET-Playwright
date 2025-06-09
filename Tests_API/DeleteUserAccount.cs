using AE_extensive_project.TestData;
using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class DeleteUserAccount : TestFixtureBase
    {
        //class-level instance so it can be used in all methods
        private TestDataGenerator.UserTestData user;

        //initialize a registration method to create user for deletion(ensuring sustainability of the delete test)
        public async Task RegisterUser()
        {
            var formData = Context.APIRequest.CreateFormData();
            user = TestDataGenerator.GenerateUser();

            //valid account creation details from faker?
            var userCredentials = new Dictionary<string, string>
            {
                {"name", user.FirstName },
                { "email", user.Email },
                { "password", user.Password },
                { "title", user.Title },
                { "birth_date", user.BirthDay.ToString() },
                { "birth_month", user.BirthMonth.ToString() },
                { "birth_year", user.BirthYear.ToString() },
                { "firstname", user.FirstName },
                { "lastname", user.LastName },
                { "company", user.Company },
                { "address1", user.Address1 },
                { "address2", user.Address2 },
                { "country", user.Country },
                { "zipcode", user.Zipcode },
                { "state", user.State },
                { "city", user.City },
                { "mobile_number", user.MobileNumber },
            };

            //update the formData with the faker params for the user creation
            foreach (var kvp in userCredentials)
            {
                formData.Set(kvp.Key, kvp.Value);
            }

            //pass the user creation details in the post request
            var response = await ApiContext.PostAsync($"{baseUrl}/api/createAccount", new APIRequestContextOptions
            {
                Form = formData
            });
        }
        [Test]
        public async Task Delete_User_Account()
        {
            /*
             * API URL: https://automationexercise.com/api/deleteAccount
                Request Method: DELETE
                Request Parameters: email, password
                Response Code: 200
                Response Message: Account deleted!
             */

            //call the user creation method from above to create user
            await RegisterUser();

            var formData = Context.APIRequest.CreateFormData();

            //requst body with empty string as email
            formData.Set("email", user.Email);
            formData.Set("password", user.Password);

            //pass the login details with DELETE request
            var response = await ApiContext.DeleteAsync($"{baseUrl}/api/deleteAccount", new APIRequestContextOptions
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

            //assert 200 response and account deleted message
            Assert.That(responseCode, Is.EqualTo(200));
            Assert.That(message, Does.Contain("account deleted").IgnoreCase);
        }
    }
}
