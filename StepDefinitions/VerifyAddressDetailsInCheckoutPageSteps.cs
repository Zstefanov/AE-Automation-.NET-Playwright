using AE_extensive_project.Helpers;
using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;
using static AE_extensive_project.TestData.TestDataGenerator;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class VerifyAddressDetailsInCheckoutPageSteps
    {
        private readonly TestFixtureBase _fixture;
        private readonly ScenarioContext _context;
        private UserTestData _user;

        public VerifyAddressDetailsInCheckoutPageSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
            _context = context;
        }

        [When("I register a new user for checkout verification")]
        public async Task WhenIRegisterANewUser()
        {
            var helper = new RegisterUser();
            _user = await helper.RegisterUserHelper(_fixture.GetPage(), _fixture.GetConfiguration()["BaseUrl"]);
            _context["user"] = _user;

            await _fixture.GetPage().Locator("a[data-qa='continue-button']").ClickAsync();
        }

        [Then("I should be logged in as that user")]
        public async Task ThenIShouldBeLoggedInAsThatUser()
        {
            var user = (UserTestData)_context["user"];
            var userDisplayLocator = _fixture.GetPage().Locator("a:has(i.fa-user)");
            await Expect(userDisplayLocator).ToContainTextAsync($"Logged in as {user.FirstName}");
        }

        [When("I delete my account")]
        public async Task WhenIDeleteMyAccount()
        {
            var page = _fixture.GetPage();
            await page.Locator("a[href='/delete_account']").ClickAsync();
        }

        [Then(@"I should see ""(.*)"" confirmation")]
        public async Task ThenIShouldSeeConfirmation(string message)
        {
            var page = _fixture.GetPage();
            await Expect(page.GetByText(message)).ToBeVisibleAsync();
            await page.ClickAsync("[data-qa='continue-button']");
        }
    }
}