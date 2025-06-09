using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class AddToCartFromRecommendedItemsSteps
    {
        private readonly TestFixtureBase _fixture;
        private string _productName = string.Empty;

        public AddToCartFromRecommendedItemsSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When("I add the first recommended item to the cart")]
        public async Task WhenIAddTheFirstRecommendedItemToTheCart()
        {
            var page = _fixture.GetPage();

            var productContainer = page.Locator("div.recommended_items .col-sm-4").First;

            _productName = await productContainer.Locator("p").InnerTextAsync();

            await productContainer.Locator("a.add-to-cart").ClickAsync();
        }

        [Then("I should see the recommended product in the cart")]
        public async Task ThenIShouldSeeTheRecommendedProductInTheCart()
        {
            var page = _fixture.GetPage();
            var productInCart = page.Locator("tr", new() { HasTextString = _productName });
            await Expect(productInCart).ToBeVisibleAsync();
        }
    }
}