using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class RemoveProductsFromCartStepDefinitions
    {
        private readonly TestFixtureBase _fixture;
        private int _initialProductCount = 3;
        private string _removedProductName;

        public RemoveProductsFromCartStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I add multiple products to the cart")]
        public async Task WhenIAddMultipleProductsToTheCart()
        {
            var page = _fixture.GetPage();

            for (int i = 0; i < _initialProductCount; i++)
            {
                var productCard = page.Locator(".features_items .product-image-wrapper").Nth(i);
                await productCard.HoverAsync();
                await productCard.Locator(".overlay-content a:has-text('Add to cart')").ClickAsync();

                if (i < _initialProductCount - 1)
                    await page.Locator("button:has-text('Continue Shopping')").ClickAsync();
            }
        }

        [Then(@"the cart should contain the correct number of products")]
        public async Task ThenTheCartShouldContainTheCorrectNumberOfProducts()
        {
            var page = _fixture.GetPage();
            await Expect(page.Locator("tr:has(td.cart_description)")).ToHaveCountAsync(_initialProductCount);
        }

        [When(@"I remove the first product from the cart")]
        public async Task WhenIRemoveTheFirstProductFromTheCart()
        {
            var page = _fixture.GetPage();
            var firstRow = page.Locator("tr:has(td.cart_description)").First;
            _removedProductName = await firstRow.Locator("td.cart_description h4 a").InnerTextAsync();
            Console.WriteLine($"Removing product: {_removedProductName}");
            await firstRow.Locator("a.cart_quantity_delete").ClickAsync();
        }

        [Then(@"the product should no longer be visible in the cart")]
        public async Task ThenTheProductShouldNoLongerBeVisibleInTheCart()
        {
            var page = _fixture.GetPage();
            await Expect(page.Locator($"tr:has-text('{_removedProductName}')")).ToHaveCountAsync(0);
        }
    }
}