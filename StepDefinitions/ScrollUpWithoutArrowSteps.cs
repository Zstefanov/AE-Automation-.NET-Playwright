using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class ScrollUpWithoutArrowSteps
    {
        private readonly TestFixtureBase _fixture;

        public ScrollUpWithoutArrowSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I scroll to the top of the page")]
        public async Task WhenIScrollToTheTopOfThePage()
        {
            var page = _fixture.GetPage();
            await page.EvaluateAsync("window.scrollTo(0, 0)");
            await page.WaitForTimeoutAsync(1000);
        }
    }
}