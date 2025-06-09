using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class InvalidLoginStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public InvalidLoginStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [Given("I am on the login page")]
        public async Task GivenIAmOnTheLoginPage()
        {
            await _fixture.NavigateToUrl();
            await _fixture.GetPage().GetByText("Signup / Login").ClickAsync();
        }

        [When("I enter invalid credentials")]
        public async Task WhenIEnterInvalidCredentials()
        {
            var email = _fixture.GetConfiguration()["InvalidCredentials:EmailAddress"];
            var password = _fixture.GetConfiguration()["InvalidCredentials:Password"];

            await _fixture.GetPage().Locator("[data-qa='login-email']").FillAsync(email);
            await _fixture.GetPage().Locator("[data-qa='login-password']").FillAsync(password);
        }

        [When("I click the login button")]
        public async Task WhenIClickTheLoginButton()
        {
            await _fixture.GetPage().Locator("[data-qa='login-button']").ClickAsync();
        }
    }
}
