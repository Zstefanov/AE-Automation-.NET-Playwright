using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class AddProductsToCartStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public AddProductsToCartStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [Given("I am logged in as a valid user")]
        public async Task GivenIAmLoggedInAsAValidUser()
        {
            string email = _fixture.GetConfiguration()["ValidCredentials:EmailAddress"];
            string password = _fixture.GetConfiguration()["ValidCredentials:Password"];
            await _fixture.Login(email, password);
        }

        [When("I add two different products to the cart")]
        public async Task WhenIAddTwoDifferentProductsToTheCart()
        {
            var page = _fixture.GetPage();

            var firstProduct = page.Locator("[data-product-id='1']").First;
            await firstProduct.HoverAsync();
            await firstProduct.ClickAsync();

            await page.ClickAsync("button.btn.btn-success.close-modal:has-text('Continue Shopping')");

            await page.Locator(".features_items").First.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            var secondProduct = page.Locator(".features_items .product-image-wrapper").Nth(1);
            await secondProduct.HoverAsync();
            await secondProduct.Locator(".product-overlay a.add-to-cart").ClickAsync();
        }

        [Then("I should see both products in the cart")]
        public async Task ThenIShouldSeeBothProductsInTheCart()
        {
            var page = _fixture.GetPage();
            await Expect(page.Locator("table.table-condensed")).ToBeVisibleAsync();

            var cartItems = page.Locator("table.table-condensed tbody tr");
            int itemCount = await cartItems.CountAsync();
            Assert.That(itemCount, Is.EqualTo(2), $"Expected 2 items, but found {itemCount}");
        }
        [Then(@"the product details and total price should be correct")]
        public async Task ThenTheProductDetailsAndTotalPriceShouldBeCorrect()
        {
            var page = _fixture.GetPage();

            var itemRows = page.Locator("table.table-condensed tbody tr[id^='product-']");
            int itemCount = await itemRows.CountAsync();

            decimal calculatedTotal = 0;

            for (int i = 0; i < itemCount; i++)
            {
                var row = itemRows.Nth(i);

                var priceText = await row.Locator(".cart_price p").InnerTextAsync();
                var quantityText = await row.Locator(".cart_quantity button").InnerTextAsync();
                var totalText = await row.Locator(".cart_total_price").InnerTextAsync();

                decimal price = decimal.Parse(priceText.Replace("Rs.", "").Trim());
                int quantity = int.Parse(quantityText.Trim());
                decimal total = decimal.Parse(totalText.Replace("Rs.", "").Trim());

                Assert.That(total, Is.EqualTo(price * quantity),
                    $"Row {i + 1}: price ({price}) x quantity ({quantity}) != total ({total})");

                calculatedTotal += total;
            }

            Console.WriteLine($"[DEBUG] Sum of all product totals = {calculatedTotal}");

            //compare against sum of .cart_total_price elements
            var totalTextElements = await page.Locator(".cart_total_price").AllInnerTextsAsync();
            decimal summedDisplayedTotal = totalTextElements
                .Select(text => decimal.Parse(text.Replace("Rs.", "").Trim()))
                .Sum();

            Assert.That(summedDisplayedTotal, Is.EqualTo(calculatedTotal),
                $"Cart total mismatch: Expected {calculatedTotal}, Found {summedDisplayedTotal}");
        }
    }
}