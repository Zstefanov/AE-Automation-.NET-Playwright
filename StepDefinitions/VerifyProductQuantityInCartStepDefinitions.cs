using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class VerifyProductQuantityInCartStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public VerifyProductQuantityInCartStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I view the first product")]
        public async Task WhenIViewTheFirstProduct()
        {
            var page = _fixture.GetPage();
            await page.Locator("text=View Product").First.ClickAsync();
        }

        [When(@"I set the quantity to (.*)")]
        public async Task WhenISetTheQuantityTo(int quantity)
        {
            var page = _fixture.GetPage();
            await page.Locator("#quantity").FillAsync(quantity.ToString());
        }

        [When(@"I add the product to the cart")]
        public async Task WhenIAddTheProductToTheCart()
        {
            var page = _fixture.GetPage();
            await page.Locator("button:has-text(\"Add to cart\")").ClickAsync();
        }

        [Then(@"the product should appear with quantity (.*) in the cart")]
        public async Task ThenTheProductShouldAppearWithQuantityInTheCart(int expectedQuantity)
        {
            var page = _fixture.GetPage();
            var quantityLocator = page.Locator("td.cart_quantity");

            await Expect(quantityLocator).ToHaveTextAsync(expectedQuantity.ToString());
        }
    }
}