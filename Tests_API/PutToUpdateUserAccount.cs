using AE_extensive_project.TestData;
using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class PutToUpdateUserAccount : TestFixtureBase
    {
        //class-level instance so it can be used in all methods
        private TestDataGenerator.UserTestData? user;

        //initialize a registration method to create user for updating(ensuring sustainability of the delete test)
        public async Task RegisterUser()
        {
            var formData = Context.APIRequest.CreateFormData();
            user = TestDataGenerator.GenerateUser();

            //valid account creation details from faker
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
        public async Task Put_To_Update_User_Account()
        {
            /*
             * API URL: https://automationexercise.com/api/updateAccount
                Request Method: PUT
                Request Parameters: name, email, password, title (for example: Mr, Mrs, Miss), birth_date, birth_month, birth_year, firstname, lastname, company, address1, address2, country, zipcode, state, city, mobile_number
                Response Code: 200
                Response Message: User updated!
             */

            //create user with the method above so we can update accordingly below
            await RegisterUser();

            var formData = Context.APIRequest.CreateFormData();

            //data for updating user with valid fake credentials
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

            //update the formData with the faker params for the user update
            foreach (var kvp in userCredentials)
            {
                formData.Set(kvp.Key, kvp.Value);
            }

            //pass the user update details in the post request
            var response = await ApiContext.PutAsync($"{baseUrl}/api/updateAccount", new APIRequestContextOptions
            {
                Form = formData
            });

            var responseBody = await response.TextAsync();

            //log status for debugging
            //TestContext.WriteLine($"Actual Status Code: {response.Status}");
            TestContext.WriteLine("Response Body: " + responseBody);

            //parse the json object we receive as a response to extract code
            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;
            var responseCode = root.GetProperty("responseCode").GetInt32();
            var message = root.GetProperty("message").GetString();

            //assert 200 response and the user updated message
            Assert.That(responseCode, Is.EqualTo(200));
            Assert.That(message, Does.Contain("user updated").IgnoreCase);
        }
    }
}
