using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.Tests
{
    public class AddToCartFromRecommendedItems : TestFixtureBase
    {
        [Test]
        public async Task Add_To_Cart_From_Recommended_Items()
        {
            await NavigateToUrl();

            //scroll to the bottom of the page
            await Page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");

            //verify 'RECOMMENDED ITEMS' are visible
            await Expect(Page.Locator("h2.title.text-center", new() 
                    { HasTextString = "recommended items" })).ToBeVisibleAsync();

            //locate the product container
            var productContainer = Page.Locator("div.recommended_items .col-sm-4").First;

            //extract product name
            var productName = await productContainer.Locator("p").InnerTextAsync();

            //click 'Add to Cart' inside that product container
            await productContainer.Locator("a.add-to-cart").ClickAsync();

            //click on view cart
            await Page.Locator("u", new() { HasTextString = "View Cart" }).ClickAsync();

            //verify that product is displayed in cart page
            var productInCart = Page.Locator("tr", new() { HasTextString = productName });
            TestContext.Out.WriteLine(productInCart.ToString());

            await Expect(productInCart).ToBeVisibleAsync();
        }
    }
}
