using TechTalk.SpecFlow;
using AE_extensive_project.TestFixture;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class ValidLoginStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public ValidLoginStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [Given("I navigate to the login page")]
        public async Task GivenINavigateToTheLoginPage()
        {
            await _fixture.NavigateToUrl();
            await _fixture.GetPage().GetByText("Signup / Login").ClickAsync();
        }

        [When("I login with valid credentials")]
        public async Task WhenILoginWithValidCredentials()
        {
            var email = _fixture.GetConfiguration()["ValidCredentials:EmailAddress"];
            var password = _fixture.GetConfiguration()["ValidCredentials:Password"];
            await _fixture.Login(email, password);
        }

        [Then("I should be logged in successfully")]
        public async Task ThenIShouldBeLoggedInSuccessfully()
        {
            var logoutVisible = await _fixture.GetPage().GetByText("Logout").IsVisibleAsync();
            Assert.That(logoutVisible, "Logout button was not visible, login might have failed.");
        }
    }
}
