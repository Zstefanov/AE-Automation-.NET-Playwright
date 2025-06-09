using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class VerifyAllProductsStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public VerifyAllProductsStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When("I click the Products link")]
        public async Task WhenIClickTheProductsLink()
        {
            await _fixture.NavigateToProductsPage();
        }

        [Then("I should see a list of products")]
        public async Task ThenIShouldSeeAListOfProducts()
        {
            await _fixture.GetPage().Locator("h2.title.text-center").IsVisibleAsync();
        }

        [When("I click the View Product link for the first product")]
        public async Task WhenIClickTheViewProductLinkForTheFirstProduct()
        {
            await _fixture.GetPage().Locator("a:has-text('View Product')").First.ClickAsync();
        }

        [Then("I should be on the Product Detail page")]
        public async Task ThenIShouldBeOnTheProductDetailPage()
        {
            await Expect(_fixture.GetPage().Locator("div.product-information")).ToBeVisibleAsync();
        }

        [Then("I should see product details like name, category, price, availability, condition, and brand")]
        public async Task ThenIShouldSeeProductDetails()
        {
            var productInfo = _fixture.GetPage().Locator("div.product-information");

            await Expect(productInfo.Locator("h2")).ToBeVisibleAsync(); // Product name
            await Expect(productInfo.GetByText("Category:")).ToBeVisibleAsync();
            await Expect(productInfo.GetByText("Availability:")).ToBeVisibleAsync();
            await Expect(productInfo.GetByText("Condition:")).ToBeVisibleAsync();
            await Expect(productInfo.GetByText("Brand:")).ToBeVisibleAsync();
        }
    }
}