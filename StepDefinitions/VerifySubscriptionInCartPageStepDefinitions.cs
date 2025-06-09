using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class VerifySubscriptionInCartPageStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public VerifySubscriptionInCartPageStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When("I navigate to the Cart page")]
        public async Task WhenINavigateToTheCartPage()
        {
            await _fixture.GetPage().ClickAsync("a:has-text('Cart')");
        }

    }
}