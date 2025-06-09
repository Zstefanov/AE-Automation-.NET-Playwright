using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class VerifyAllProductsAndProductDetailPage : TestFixtureBase
    {
        
        [Test]
        public async Task Verify_All_Products_And_Product_Detail_Page()
        {           
            await NavigateToUrl();

            //click on 'Products' button
            await NavigateToProductsPage();

            //verify user is navigated to ALL PRODUCTS page successfully
            await Expect(Page.Locator("h2:has-text('All Products')")).ToBeVisibleAsync();

            //the products list is visible
            await Page.Locator("h2.title.text-center").IsVisibleAsync();

            //click on 'View Product' of first product
            await Page.Locator("a:has-text('View Product')").First.ClickAsync();

            //user is landed to product detail page
            await Expect(Page.Locator("div.product-information")).ToBeVisibleAsync();

            //verify that details are visible: product name, category, price, availability, condition, brand
            var productInfo = Page.Locator("div.product-information");
            await Expect(productInfo.Locator("h2")).ToBeVisibleAsync(); // Product name
            await Expect(productInfo.GetByText("Category:")).ToBeVisibleAsync();
            await Expect(productInfo.GetByText("Availability:")).ToBeVisibleAsync();
            await Expect(productInfo.GetByText("Condition:")).ToBeVisibleAsync();
            await Expect(productInfo.GetByText("Brand:")).ToBeVisibleAsync();
        }
    }
}
