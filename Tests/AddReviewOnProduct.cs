using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.Tests
{
    public class AddReviewOnProduct : TestFixtureBase
    {
        [Test]
        public async Task Add_Review_On_Product()
        {
            //navigate to products page from home page
            await NavigateToUrl();
            await NavigateToProductsPage();

            //verify user is navigated to ALL PRODUCTS page successfully
            await Expect(Page.Locator("h2:has-text('All Products')")).ToBeVisibleAsync();

            //click on view product - any on the page , doesn't matter
            await Page.GetByRole(AriaRole.Link, new() { Name = "View Product" }).Nth(0).ClickAsync();

            //verify 'Write Your Review' is visible
            await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Write Your Review" })).ToBeVisibleAsync();

            //fill in random data in the fields
            await Page.Locator("#name").FillAsync("test");
            await Page.Locator("#email").FillAsync("test@test.com");
            await Page.Locator("#review").FillAsync("This is my review for the product.");

            //click 'Submit' button
            await Page.Locator("#button-review").ClickAsync();

            //verify success message 'Thank you for your review.'
            await Expect(Page.Locator("span", new() { 
                HasTextString = "Thank you for your review." })).ToBeVisibleAsync();
        }
    }
}
