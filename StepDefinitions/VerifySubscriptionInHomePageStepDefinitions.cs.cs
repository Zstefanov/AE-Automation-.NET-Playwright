using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class VerifySubscriptionInHomePageStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public VerifySubscriptionInHomePageStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [Given("the homepage is fully loaded")]
        public async Task GivenTheHomepageIsFullyLoaded()
        {
            var isVisible = await _fixture.GetPage().Locator("#slider").IsVisibleAsync();
            Assert.That(isVisible, "Expected homepage slider to be visible.");
        }
    }
}