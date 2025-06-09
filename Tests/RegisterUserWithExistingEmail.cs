using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class RegisterUserWithExistingEmail : TestFixtureBase
    {
        [Test]
        public async Task Register_User_With_Existing_Email()
        {
            //from base class
            await NavigateToUrl();

            //click the signup/login
            await Page.GetByText("Signup / Login").ClickAsync();

            //verify 'New User Signup!' is visible
            await Page.Locator("text=New User Signup!").IsVisibleAsync();

            //valid username from appsettings.json
            Username = Configuration["ValidCredentials:Username"];
            await Page.Locator("input[data-qa='signup-name']").FillAsync(Username);
            
            //valid email from appsettings.json
            EmailAddress = Configuration["ValidCredentials:EmailAddress"];
            await Page.Locator("input[data-qa='signup-email']").FillAsync(EmailAddress);

            //click 'Signup' button
            await Page.Locator("button[data-qa='signup-button']").ClickAsync();

            //assert error 'Email Address already exist!' is visible
            var errorMsg = Page.Locator("text=Email Address already exist!");
            await Expect(errorMsg).ToBeVisibleAsync();
        }
    }
}
