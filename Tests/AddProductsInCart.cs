using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.Tests
{
    public class AddProductsInCart : TestFixtureBase
    {
        [Test]
        public async Task Add_Products_In_Cart()
        {
            //valid login details from appsettings.json
            string email = Configuration["ValidCredentials:EmailAddress"];
            string password = Configuration["ValidCredentials:Password"];

            //login automatically navigates to home page as well
            await Login(email, password);

            //verify home page is visible successfully via unique element
            Assert.That(await Page.Locator("#slider").IsVisibleAsync());

            //click 'Products' button
            await NavigateToProductsPage();

            //hover over first product and click 'Add to cart'
            //hover over the product container first (if required for button visibility)
            var product = Page.Locator("[data-product-id='1']").First;
            await product.HoverAsync();

            //click the 'Add to cart' button
            await product.ClickAsync();

            //click 'Continue Shopping' button
            await Page.ClickAsync("button.btn.btn-success.close-modal:has-text('Continue Shopping')");

            //hover over second product and click 'Add to cart'
            await Page.Locator(".features_items").First.WaitForAsync(new() { State = WaitForSelectorState.Visible });
            var secondProduct = Page.Locator(".features_items .product-image-wrapper").Nth(1);
            await secondProduct.HoverAsync();
            await secondProduct.Locator(".product-overlay a.add-to-cart").ClickAsync();

            // click 'View Cart' button
            await Page.ClickAsync("u:has-text('View Cart')");

            //verify both products are added to cart
            await Expect(Page.Locator("table.table-condensed")).ToBeVisibleAsync();
            //get all rows in the cart table (except first one as it is description)
            var cartItems = Page.Locator("table.table-condensed tbody tr");
            int itemCount = await cartItems.CountAsync();

            //assert expected count (in this case 2)
            Assert.That(itemCount, Is.EqualTo(2), $"Expected cart to have items, but found {itemCount}");

            //Login in order to be able to proceed to checkout
            await Page.ClickAsync("a:has-text('Proceed To Checkout')");

            //verify item prices, quantity and total price of items in the cart
            //locate the cart rows for individual items only (exclude the final total row)
            var finalItemRows = Page.Locator("table.table-condensed tbody tr[id^='product-']");
            int finalItemCount = await finalItemRows.CountAsync();

            decimal calculatedTotal = 0;

            //loop through each product row and accumulate the total
            for (int i = 0; i < finalItemCount; i++)
            {
                var row = finalItemRows.Nth(i);

                //extract price and quantity
                var priceText = await row.Locator(".cart_price p").InnerTextAsync();
                var quantityText = await row.Locator(".cart_quantity button").InnerTextAsync();
                var totalText = await row.Locator(".cart_total_price").InnerTextAsync();

                //convert
                decimal price = decimal.Parse(priceText.Replace("Rs.", "").Trim());
                int quantity = int.Parse(quantityText.Trim());
                decimal total = decimal.Parse(totalText.Replace("Rs.", "").Trim());

                //verify total per item
                Assert.That(total, Is.EqualTo(price * quantity),
                    $"Row {i + 1}: price ({price}) x quantity ({quantity}) != total ({total})");

                calculatedTotal += total;
            }

            //extract the total cart price (last row)
            var cartTotalText = await Page.Locator("table.table-condensed tbody tr:last-child .cart_total_price").InnerTextAsync();
            decimal displayedTotal = decimal.Parse(cartTotalText.Replace("Rs.", "").Trim());

            //final assertion
            Assert.That(displayedTotal, Is.EqualTo(calculatedTotal),
                $"Cart total mismatch: Expected {calculatedTotal}, Found {displayedTotal}");
        }
    }
}