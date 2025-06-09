using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class RegisterExistingUserStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public RegisterExistingUserStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When("I enter an existing username and email address")]
        public async Task WhenIEnterAnExistingUsernameAndEmailAddress()
        {
            var username = _fixture.GetConfiguration()["ValidCredentials:Username"];
            var email = _fixture.GetConfiguration()["ValidCredentials:EmailAddress"];

            await _fixture.GetPage().Locator("input[data-qa='signup-name']").FillAsync(username);
            await _fixture.GetPage().Locator("input[data-qa='signup-email']").FillAsync(email);
        }
    }
}
