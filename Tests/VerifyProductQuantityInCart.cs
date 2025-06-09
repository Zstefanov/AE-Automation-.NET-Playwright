using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class VerifyProductQuantityInCart : TestFixtureBase
    {
        [Test]
        public async Task Verify_Product_Quantity_In_Cart()
        {
            await NavigateToUrl();

            //verify home page is visible successfully via unique element
            Assert.That(await Page.Locator("#slider").IsVisibleAsync());

            //click 'View Product' for any product on home page
            await Page.Locator("text=View Product").Nth(0).ClickAsync();

            //increase quantity to 4
            await Page.Locator("#quantity").FillAsync("4");

            //click 'Add to cart' button
            await Page.Locator("button:has-text(\"Add to cart\")").ClickAsync();

            //click 'View Cart' button
            await Page.Locator("text=View Cart").ClickAsync();

            //verify that product is displayed in cart page with exact quantity
            var quantityLocator = Page.Locator("td.cart_quantity");

            await Expect(quantityLocator).ToHaveTextAsync("4");
        }
    }
}
