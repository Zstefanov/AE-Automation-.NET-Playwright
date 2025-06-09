using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.Tests
{
    public class LoginUserWithIncorrectEmailPass : TestFixtureBase
    {
        [Test]
        public async Task LoginUserWithIncorrectEmailAndPassword()
        {
            //invalid user details appsettings.json
            Password = Configuration["InvalidCredentials:Password"];
            EmailAddress = Configuration["InvalidCredentials:EmailAddress"];

            await NavigateToUrl();
            await Page.GetByText("Signup / Login").ClickAsync();

            await Page.Locator("[data-qa='login-email']").FillAsync(EmailAddress);
            await Page.Locator("[data-qa='login-password']").FillAsync(Password);
            await Page.Locator("[data-qa='login-button']").ClickAsync();

            //assert we see the error message
            var invalidLoginMsg = Page.GetByText("Your email or password is incorrect!", new() 
            { Exact = true });

            await Expect(invalidLoginMsg).ToBeVisibleAsync();
        }
    }
}
