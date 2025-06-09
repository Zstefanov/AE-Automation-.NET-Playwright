using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class SearchProductDataDrivenSteps
    {
        private readonly TestFixtureBase _fixture;

        public SearchProductDataDrivenSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I search for ""(.*)"" from external data")]
        public async Task WhenISearchForFromJson(string searchTerm)
        {
            await _fixture.GetPage().FillAsync("input[name='search']", searchTerm);
            await _fixture.GetPage().ClickAsync("button[type='submit']");
        }

        [Then(@"the product ""(.*)"" should be (true|false)")]
        public async Task ThenSearchShouldReturnExpected(string searchTerm, string expectedResult)
        {
            var locator = _fixture.GetPage().Locator(".features_items .productinfo p");
            var results = await locator.AllInnerTextsAsync();

            bool contains = results.Any(x =>
                x.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            if (bool.Parse(expectedResult))
                Assert.That(contains, $"Expected to find '{searchTerm}' but didn't.");
            else
                Assert.That(contains, Is.False, $"Expected NOT to find '{searchTerm}' but did.");
        }
    }
}