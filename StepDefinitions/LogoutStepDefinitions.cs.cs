using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class LogoutStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public LogoutStepDefinitions(ScenarioContext scenarioContext)
        {
            _fixture = (TestFixtureBase)scenarioContext["fixture"];
        }

        [Given("I am logged in with valid credentials")]
        public async Task GivenIAmLoggedInWithValidCredentials()
        {
            var email = _fixture.GetConfiguration()["ValidCredentials:EmailAddress"];
            var password = _fixture.GetConfiguration()["ValidCredentials:Password"];
            await _fixture.Login(email, password);

            var logoutLink = _fixture.GetPage().Locator("a[href='/logout']");
            await Expect(logoutLink).ToBeVisibleAsync();
        }

        [When("I click the logout button")]
        public async Task WhenIClickTheLogoutButton()
        {
            await _fixture.Logout();
        }

        [Then("I should be redirected to the login page")]
        public async Task ThenIShouldBeRedirectedToTheLoginPage()
        {
            var loginPage = _fixture.GetPage().Locator("text=Signup / Login");
            await Expect(loginPage).ToBeVisibleAsync();
        }
    }

}
