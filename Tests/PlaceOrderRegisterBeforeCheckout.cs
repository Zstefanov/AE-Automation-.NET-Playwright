using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using static AE_extensive_project.TestData.TestDataGenerator;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class PlaceOrderRegisterBeforeCheckout : TestFixtureBase
    {
        private UserTestData user;
        public async Task Register_User()
        {
            var registerHelper = new Helpers.RegisterUser();

            //create user and use it below
            user = await registerHelper.RegisterUserHelper(Page, baseUrl);
            Console.WriteLine($"Registered user: {user.FirstName} {user.LastName}, Email: {user.Email}");
        }

        [Test]
        public async Task Place_Order_Register_Before_Checkout()
        {
            await NavigateToUrl();

            //verify home page is visible successfully via unique element
            Assert.That(await Page.Locator("#slider").IsVisibleAsync());

            //create user and verify created message from helpers.RegisterUser class
            await Register_User();

            //click continue to be navigated to the home page
            await Page.Locator("a[data-qa='continue-button']").ClickAsync();

            //verify ' Logged in as {username} at the top of page
            var userDisplayLocator = Page.Locator("a:has(i.fa-user)");
            await Expect(userDisplayLocator).ToContainTextAsync($"Logged in as {user.FirstName}");

            //add products to cart
            await Page.Locator("a[data-product-id='1']").First.ClickAsync();

            //click 'Cart' button
            await Page.Locator("text=View Cart").ClickAsync();

            //click Proceed To Checkout
            await Page.Locator("text=Proceed To Checkout").ClickAsync();

            //verify address details and review order
            //locate both address blocks
            var deliveryAddressLocator = Page.Locator("#address_delivery li");
            var billingAddressLocator = Page.Locator("#address_invoice li");

            //get inner texts as string arrays
            var deliveryLines = await deliveryAddressLocator.AllInnerTextsAsync();
            var billingLines = await billingAddressLocator.AllInnerTextsAsync();

            //compare addresses
            bool addressesMatch = deliveryLines.SequenceEqual(billingLines);

            if (addressesMatch)
            {
                Console.WriteLine("Delivery and Billing addresses match.");
            }
            else
            {
                Console.WriteLine("Delivery and Billing addresses do NOT match.");
            }

            //enter description in comment text area and click 'Place Order'
            await Page.Locator("textarea[name='message']").FillAsync("This is a test message.");
            await Page.GetByRole(AriaRole.Link, new() { Name = "Place Order" }).ClickAsync();

            //enter payment details: Name on Card, Card Number, CVC, Expiration date
            await Page.Locator("input[data-qa='name-on-card']").FillAsync(user.FirstName);
            await Page.Locator("input[data-qa='card-number']").FillAsync("1234 5678 1234 5678");
            await Page.Locator("input[data-qa='cvc']").FillAsync("999");
            await Page.Locator("input[data-qa='expiry-month']").FillAsync("12");
            await Page.Locator("input[data-qa='expiry-year']").FillAsync("2030");

            //click 'Pay and Confirm Order' button
            await Page.GetByRole(AriaRole.Button, new() { Name = "Pay and Confirm Order" }).ClickAsync();

            //locate text for confirmation of order
            await Page.Locator("text=Congratulations! Your order has been confirmed!").ClickAsync();

            //click 'Delete Account' button
            await Page.Locator("a[href='/delete_account']").ClickAsync();

            //verify 'ACCOUNT DELETED!' and click 'Continue' button
            await Expect(Page.GetByText("Account Deleted!")).ToBeVisibleAsync();
            await Page.ClickAsync("[data-qa='continue-button']");
        }
    }
}
