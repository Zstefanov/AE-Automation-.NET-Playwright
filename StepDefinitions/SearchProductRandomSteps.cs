using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class SearchProductRandomSteps
    {
        private readonly TestFixtureBase _fixture;
        private string _selectedProduct = string.Empty;

        public SearchProductRandomSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When("I search for a randomly selected product")]
        public async Task WhenISearchForARandomProduct()
        {
            var productNames = await _fixture.GetPage().Locator(".productinfo p").AllInnerTextsAsync();
            var random = new Random();
            _selectedProduct = productNames[random.Next(productNames.Count)];

            await _fixture.GetPage().FillAsync("input[name='search']", _selectedProduct);
            await _fixture.GetPage().ClickAsync("button[type='submit']");
        }

        [Then("the search results should contain that product")]
        public async Task ThenSearchResultsShouldContainRandom()
        {
            var result = _fixture.GetPage().Locator(".productinfo p", new() { HasTextString = _selectedProduct });
            Assert.That(await result.IsVisibleAsync(), $"Search did not return expected product: {_selectedProduct}");
        }
    }
}