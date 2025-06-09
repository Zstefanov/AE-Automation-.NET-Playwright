using AE_extensive_project.TestFixture;
using AE_extensive_project.Helpers;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class RegisterUserStepDefinitions
    {
        private readonly TestFixtureBase _fixture;
        private RegisterUser _registerHelper;

        public RegisterUserStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
            _registerHelper = new RegisterUser();
        }

        [When("I enter a valid name and email address")]
        public async Task WhenIEnterAValidNameAndEmailAddress()
        {
            var page = _fixture.GetPage();
            var user = AE_extensive_project.TestData.TestDataGenerator.GenerateUser();

            _fixture.TestUser = user;

            await page.Locator("input[data-qa='signup-name']").FillAsync(user.FirstName);
            await page.Locator("input[data-qa='signup-email']").FillAsync(user.Email);
        }

        [When("I fill in all required personal and address details")]
        public async Task WhenIFillInAllRequiredDetails()
        {
            var user = _fixture.TestUser;
            var page = _fixture.GetPage();

            await page.Locator("#id_gender1").ClickAsync();
            await page.Locator("#password").FillAsync(user.Password);
            await page.Locator("#days").SelectOptionAsync("10");
            await page.Locator("#months").SelectOptionAsync("5");
            await page.Locator("#years").SelectOptionAsync("1990");
            await page.Locator("#newsletter").SetCheckedAsync(true);
            await page.Locator("#optin").SetCheckedAsync(true);
            await page.Locator("#first_name").FillAsync(user.FirstName);
            await page.Locator("#last_name").FillAsync(user.LastName);
            await page.Locator("#company").FillAsync(user.Company);
            await page.Locator("#address1").FillAsync("3227 Melody Lane");
            await page.Locator("#address2").FillAsync("3227 Melody Lane");
            await page.Locator("#country").SelectOptionAsync("United States");
            await page.Locator("#state").FillAsync("Virginia");
            await page.Locator("#city").FillAsync("Richmond");
            await page.Locator("#zipcode").FillAsync("23173");
            await page.Locator("#mobile_number").FillAsync(user.MobileNumber);
        }

        [When("I click the Create Account button")]
        public async Task WhenIClickTheCreateAccountButton()
        {
            await _fixture.GetPage().Locator("button[data-qa='create-account']").ClickAsync();
        }

        [Then("I should see a confirmation message saying \"(.*)\"")]
        public async Task ThenIShouldSeeAConfirmationMessageSaying(string expectedMessage)
        {
            var confirmationText = _fixture.GetPage().GetByText(expectedMessage);
            await Expect(confirmationText).ToBeVisibleAsync();
            await Expect(confirmationText).ToContainTextAsync(expectedMessage);
        }
    }
}
