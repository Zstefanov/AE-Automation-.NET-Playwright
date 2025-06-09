using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using static AE_extensive_project.TestData.TestDataGenerator;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class PlaceOrderRegisterWhileCheckoutStepDefinitions
    {
        private readonly TestFixtureBase _fixture;
        private readonly ScenarioContext _scenarioContext;
        private UserTestData _user;

        public PlaceOrderRegisterWhileCheckoutStepDefinitions(ScenarioContext context)
        {
            _scenarioContext = context;
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I choose to register during checkout")]
        public async Task WhenIChooseToRegisterDuringCheckout()
        {
            var page = _fixture.GetPage();
            await page.GetByRole(AriaRole.Link, new() { Name = "Register / Login" }).ClickAsync();
        }

        [When(@"I complete registration")]
        public async Task WhenICompleteRegistration()
        {
            var registerHelper = new Helpers.RegisterUser();
            _user = await registerHelper.RegisterUserHelper(_fixture.GetPage(), _fixture.GetConfiguration()["baseUrl"]);
            Console.WriteLine($"Registered user: {_user.FirstName} {_user.LastName}, Email: {_user.Email}");

            _scenarioContext["user"] = _user;

            await _fixture.GetPage().Locator("a[data-qa='continue-button']").ClickAsync();
        }

        [Then(@"I should be logged in after registration")]
        public async Task ThenIShouldBeLoggedInAfterRegistration()
        {
            var page = _fixture.GetPage();
            var userDisplayLocator = page.Locator("a:has(i.fa-user)");
            await Expect(userDisplayLocator).ToContainTextAsync($"Logged in as {_user.FirstName}");
        }

        [When(@"I proceed to checkout again")]
        public async Task WhenIProceedToCheckoutAgain()
        {
            var page = _fixture.GetPage();
            await page.GetByRole(AriaRole.Link, new() { Name = "Cart" }).ClickAsync();
            await page.Locator("a.btn.btn-default.check_out").ClickAsync();
        }

        [Then(@"I delete the account")]
        public async Task ThenIDeleteTheAccount()
        {
            var page = _fixture.GetPage();
            await page.Locator("a[href='/delete_account']").ClickAsync();
        }

        [Then(@"the account should be deleted successfully")]
        public async Task ThenTheAccountShouldBeDeletedSuccessfully()
        {
            var page = _fixture.GetPage();
            await Expect(page.GetByText("Account Deleted!")).ToBeVisibleAsync();
            await page.ClickAsync("[data-qa='continue-button']");
        }
    }
}