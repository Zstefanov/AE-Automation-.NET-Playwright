using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class ViewCategoryProducts : TestFixtureBase
    {
        [Test]
        public async Task View_Category_Products()
        {
            await NavigateToUrl();

            //verify categories are visible 
            var categoryHeading = Page.Locator("h2", new() { HasTextString = "Category" });
            await Expect(categoryHeading).ToBeVisibleAsync();

            //click on "women" category
            await Page.Locator("div.left-sidebar .panel-group a[href='#Women']").ClickAsync();

            //click on Dress option
            await Page.Locator("#Women ul li a:has-text('Dress')").ClickAsync();

            //verify that the title is adequate and we are viewing the women tops
            string womenTopsText = await Page.Locator("h2.title.text-center").InnerTextAsync();
            Assert.That(womenTopsText, Does.Contain("WOMEN - DRESS PRODUCTS").IgnoreCase);

            //go to men and select jeans subcategory
            await Page.Locator("a[href='#Men']").ClickAsync();
            await Page.Locator("#Men ul li a:has-text('Jeans')").ClickAsync();

            //verify that the title is adequate and we are viewing mens jeans
            string menJeansText = await Page.Locator("h2.title.text-center").InnerTextAsync();
            Assert.That(menJeansText, Does.Contain("Men - Jeans Products").IgnoreCase);
        }
    }
}
