using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class RemoveProductsFromCart : TestFixtureBase
    {
        [Test]
        public async Task Remove_Products_From_Cart()
        {
            //navigate to url
            await NavigateToUrl();

            //verify home page is visible successfully via unique element
            Assert.That(await Page.Locator("#slider").IsVisibleAsync());

            //we add more than one product to ensure logic that is flexible regardless of how many items we have in our cart
            int numberOfProductsToAddToCart = 3;
            for (int i = 0; i < numberOfProductsToAddToCart; i++)
            {
                var productCard = Page.Locator(".features_items .product-image-wrapper").Nth(i);

                await productCard.HoverAsync();

                await productCard.Locator(".overlay-content a:has-text('Add to cart')").ClickAsync();

                //continue shopping, only until 2, so we can click cart popup
                if (i < numberOfProductsToAddToCart -1 )
                {
                    await Page.Locator("button:has-text('Continue Shopping')").ClickAsync();
                }
            }

            //click 'Cart' button
            await Page.Locator("text=View Cart").ClickAsync();

            //verify that we are on cart page
            Assert.That(Page.Url, Does.Contain("/view_cart"));
            await Expect(Page.Locator("li.active:has-text('Shopping Cart')")).ToBeVisibleAsync();

            //verify we have exact number of items added as requested above
            await Expect(Page.Locator("tr:has(td.cart_description)")).ToHaveCountAsync(numberOfProductsToAddToCart);

            //locate first product row
            var firstRow = Page.Locator("tr:has(td.cart_description)").First;

            //extract product name
            var productName = await firstRow.Locator("td.cart_description h4 a").InnerTextAsync();
            Console.WriteLine($"Removing product: {productName}");

            //remove the product from the row
            await firstRow.Locator("a.cart_quantity_delete").ClickAsync();

            //verify that row/ product is gone
            await Expect(Page.Locator($"tr:has-text('{productName}')")).ToHaveCountAsync(0);
        }
    }
}
