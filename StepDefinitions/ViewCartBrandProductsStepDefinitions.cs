using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class ViewCartBrandProductsStepDefinitions
    {
        private readonly TestFixtureBase _fixture;
        private IPage Page => _fixture.GetPage();

        public ViewCartBrandProductsStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [Then(@"I should see the Brands section")]
        public async Task ThenIShouldSeeTheBrandsSection()
        {
            var brandsSection = Page.Locator("div.brands_products");
            await Expect(brandsSection).ToBeVisibleAsync();
        }

        [When(@"I select the ""(.*)"" brand")]
        public async Task WhenISelectTheBrand(string brandName)
        {
            await Page.Locator($"div.brands_products a:has-text('{brandName}')").ClickAsync();
        }

        [Then(@"I should see products filtered by brand ""(.*)""")]
        public async Task ThenIShouldSeeProductsFilteredByBrand(string brandName)
        {
            var header = Page.Locator("h2.title.text-center");
            var text = await header.InnerTextAsync();
            await Expect(header).ToContainTextAsync(brandName, new() { IgnoreCase = true });
        }
    }
}