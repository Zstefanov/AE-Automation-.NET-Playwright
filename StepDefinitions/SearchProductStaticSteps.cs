using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class SearchProductStaticSteps
    {
        private readonly TestFixtureBase _fixture;

        public SearchProductStaticSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [Then(@"the search results should contain ""(.*)""")]
        public async Task ThenTheSearchResultsShouldContain(string productName)
        {
            var matchedProducts = _fixture.GetPage().Locator(".features_items .productinfo p");
            var productTexts = await matchedProducts.AllInnerTextsAsync();

            Assert.That(productTexts.Any(name =>
                name.Contains(productName, StringComparison.OrdinalIgnoreCase)),
                $"Search results do not contain expected product: {productName}");
        }
    }
}