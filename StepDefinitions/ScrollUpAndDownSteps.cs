using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class ScrollUpAndDownSteps
    {
        private readonly TestFixtureBase _fixture;

        public ScrollUpAndDownSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I scroll to the bottom of the page")]
        public async Task WhenIScrollToTheBottomOfThePage()
        {
            var page = _fixture.GetPage();
            await page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");
        }

        [When(@"I click the scroll up arrow")]
        public async Task WhenIClickTheScrollUpArrow()
        {
            var page = _fixture.GetPage();
            await page.Locator("#scrollUp").ClickAsync();
            await page.WaitForTimeoutAsync(1000);
        }

        [Then(@"I should see the top message ""([^""]*)""")]
        public async Task ThenIShouldSeeTheTopMessage(string message)
        {
            var page = _fixture.GetPage();
            await Microsoft.Playwright.Assertions.Expect(page.Locator($"text={message}").Nth(0)).ToBeVisibleAsync();
        }
    }
}