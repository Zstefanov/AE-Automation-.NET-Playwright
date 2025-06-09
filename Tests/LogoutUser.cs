using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.Tests
{
    public class LogoutUser : TestFixtureBase
    {
        [Test]
        public async Task Logout_User()
        {
            //valid login details from appsettings.json
            string email = Configuration["ValidCredentials:EmailAddress"];
            string password = Configuration["ValidCredentials:Password"];

            //login from fixture base class with valid cred
            await Login(email, password);

            //assert we see the logout option
            var logoutLink = Page.Locator("a[href='/logout']");
            await Expect(logoutLink).ToBeVisibleAsync();

            //logout from fixture base class
            await Logout();

            //validate that we see the login option(meaning we logged out)
            var loginPage = Page.Locator("text=Signup / Login");
            await Expect(loginPage).ToBeVisibleAsync();
        }
    }
}