using AE_extensive_project.TestFixture;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class VerifySubscriptionInCartPage : TestFixtureBase
    {
        [Test]
        public async Task Verify_Subscription_In_Cart_Page()
        {
            await NavigateToUrl();

            //verify home page is visible successfully via unique element
            Assert.That(await Page.Locator("#slider").IsVisibleAsync());

            //click 'Cart' button
            await Page.ClickAsync("a:has-text('Cart')");

            //scroll down to footer
            await Page.Locator("div.footer-widget").ScrollIntoViewIfNeededAsync();

            // verify text 'SUBSCRIPTION'
            await Page.Locator("#susbscribe_email").IsVisibleAsync();

            //enter random email address in input and click arrow button
            await Page.Locator("#susbscribe_email").FillAsync("testEmail@mail.com");
            await Page.ClickAsync("#subscribe");

            //verify message 'You have been successfully subscribed!' is visible
            var successMessage = Page.Locator("div.alert-success.alert");
            await Expect(successMessage).ToBeVisibleAsync();
        }
    }
}
