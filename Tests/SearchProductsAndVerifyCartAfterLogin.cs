using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class SearchProductsAndVerifyCartAfterLogin : TestFixtureBase
    {
        [Test]
        public async Task Search_Products_And_Verify_Cart_After_Login()
        {
            await NavigateToUrl();

            //click on products page
            await Page.Locator("a[href='/products']:has-text('Products')").ClickAsync();

            //verify user is navigated to ALL PRODUCTS page successfully
            await Expect(Page.Locator("h2.title.text-center")).ToBeVisibleAsync();
            await Expect(Page.Locator("h2.title.text-center")).ToHaveTextAsync("All Products", new() { IgnoreCase = true });

            //enter product name in search input and click search button
            var productToSearch = "Men Tshirt";
            await Page.Locator("#search_product").FillAsync(productToSearch);
            await Page.Locator("i.fa.fa-search").ClickAsync();

            //verify 'SEARCHED PRODUCTS' is visible
            await Expect(Page.Locator("div.productinfo p", new() { HasTextString = productToSearch }))
                .ToBeVisibleAsync();

            //аdd those products to cart
            await Page.Locator("a.add-to-cart[data-product-id='2']:visible").First.ClickAsync();

            //click open cart to view the product
            await Page.Locator("u", new() { HasTextString = "View Cart" }).ClickAsync();
            
            //valid login details from appsettings.json
            string email = Configuration["ValidCredentials:EmailAddress"];
            string password = Configuration["ValidCredentials:Password"];
            await Login(email, password);

            //navigate to cart page
            await Page.GetByRole(AriaRole.Link, new() { Name = "Cart" }).ClickAsync();

            //verify that the products we chose are visible in cart after login as well
            var productInCart = Page.Locator("tr", new() { HasTextString = "Men Tshirt" });
            await Expect(productInCart).ToBeVisibleAsync();
        }
    }
}
