using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class ScrollUpWithoutArrowAndScrollDown : TestFixtureBase
    {
        [Test]
        public async Task Scroll_Up_Without_Arrow_And_Scroll_Down()
        {
            await NavigateToUrl();

            //verify home page is visible successfully via unique element
            Assert.That(await Page.Locator("#slider").IsVisibleAsync());

            //scroll down to the bottom of the page
            await Page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight)");

            //verify "Subscriptions" is visible
            await Expect(Page.Locator("h2", new()
            { HasTextString = "Subscription" })).ToBeVisibleAsync();

            //scroll to the top of the page
            await Page.EvaluateAsync("window.scrollTo(0, 0)");

            //optional timeout to ensure time for the page to scroll
            await Page.WaitForTimeoutAsync(1000);

            //assert that 'Full-Fledged practice website for Automation Engineers' is visible on screen
            await Expect(Page.Locator("text=Full-Fledged practice website for Automation Engineers").
                Nth(0)).ToBeVisibleAsync();
        }
    }
}
