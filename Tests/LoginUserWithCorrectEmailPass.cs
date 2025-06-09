using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.Tests
{
    public class LoginUserWithCorrectEmailPass : TestFixtureBase
    {
        [Test]
        public async Task LoginUserWithCorrectEmailAndPassword()
        {
            //valid login details from appsettings.json
            string email = Configuration["ValidCredentials:EmailAddress"];
            string password = Configuration["ValidCredentials:Password"];
            
            //login from fixture base class with valid cred
            await Login(email, password);

            //assert we see logout option(meaning that we successfully logged in)
            var logoutLink = Page.Locator("a[href='/logout']");
            await Expect(logoutLink).ToBeVisibleAsync();
        }
    }
}
