using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class ViewCartBrandProducts : TestFixtureBase
    {
        public async Task CLickBrandNameAsync(string brandName)
        {
            await Page.Locator($"div.brands_products a:has-text('{brandName}')").ClickAsync();
        }

        [Test]
        public async Task View_Cart_Brand_Products()
        {
            await NavigateToUrl();

            //click on products page
            await Page.Locator("a[href='/products']:has-text('Products')").ClickAsync();

            //verify that Brands are visible on left side bar
            var brandsSection = Page.Locator("div.brands_products");
            await Expect(brandsSection).ToBeVisibleAsync();

            //click on any brand name
            await CLickBrandNameAsync("H&M");

            //verify that filtered products are shown with the brand name in heading
            var productHeader = Page.Locator("h2.title.text-center");
            var headerText = await productHeader.InnerTextAsync();

            //verify we are on the correct brand page
            await Expect(productHeader).ToContainTextAsync(headerText, new() { IgnoreCase = true });
            
            //on left side bar, click on any other brand link
            await CLickBrandNameAsync("POLO");

            //reassign the values based on the new brand selected
            productHeader = Page.Locator("h2.title.text-center");
            headerText = await productHeader.InnerTextAsync();

            //verify we are on the correct brand page
            await Expect(productHeader).ToContainTextAsync(headerText, new() { IgnoreCase = true });
        }
    }
}
