using TechTalk.SpecFlow;
using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;
using static AE_extensive_project.TestData.TestDataGenerator;

namespace AE_extensive_project.StepDefinitions
{
    //this file is created to avoid ambiguity in same steps across the feature files
    [Binding]
    public class CommonSteps
    {
        private readonly TestFixtureBase _fixture;
        private readonly ScenarioContext _scenarioContext;

        public CommonSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
            _scenarioContext = context;
        }

        [Given("I am on the homepage")]
        public async Task GivenIAmOnTheHomepage()
        {
            await _fixture.NavigateToUrl();
        }

        [When(@"I search for ""(.*)""")]
        public async Task WhenISearchFor(string product)
        {
            var page = _fixture.GetPage();
            await page.Locator("#search_product").FillAsync(product);
            await page.Locator("i.fa.fa-search").ClickAsync();
        }

        [When(@"I go to the cart and proceed to checkout")]
        public async Task WhenIGoToTheCartAndProceedToCheckout()
        {
            var page = _fixture.GetPage();

            //click 'Cart' button
            await page.GetByRole(AriaRole.Link, new() { Name = "Cart" }).ClickAsync();

            //click 'Proceed To Checkout' button
            await page.Locator("a.btn.btn-default.check_out").ClickAsync();
        }

        [Then(@"I should see the ""([^""]*)"" section")]
        public async Task ThenIShouldSeeTheSection(string sectionTitle)
        {
            var page = _fixture.GetPage();

            //generic locator that works for headers like <h2>Subscription</h2>
            var locator = page.Locator($":text('{sectionTitle}')");

            //wait and assert it's visible
            await locator.WaitForAsync(new() { Timeout = 10000 });
            await Expect(locator).ToBeVisibleAsync();
        }

        [When(@"I view the cart")]
        public async Task WhenIViewTheCart()
        {
            var page = _fixture.GetPage();
            await page.Locator("text=View Cart").ClickAsync();
            Assert.That(page.Url, Does.Contain("/view_cart"));
            await Expect(page.Locator("li.active:has-text('Shopping Cart')")).ToBeVisibleAsync();
        }

        [When("I navigate to the Signup/Login page")]
        public async Task WhenINavigateToTheSignupLoginPage()
        {
            await _fixture.GetPage().GetByText("Signup / Login").ClickAsync();
        }
        [When("I scroll to the footer")]
        public async Task WhenIScrollToTheFooter()
        {
            await _fixture.GetPage().Locator("div.footer-widget").ScrollIntoViewIfNeededAsync();
        }

        [When(@"I go to the Products page")]
        public async Task WhenIGoToTheProductsPage()
        {
            await _fixture.GetPage().Locator("a[href='/products']:has-text('Products')").ClickAsync();
        }

        [Then(@"I should see the All Products page")]
        public async Task ThenIShouldSeeTheAllProductsPage()
        {
            var title = _fixture.GetPage().Locator("h2.title.text-center");
            await Expect(title).ToBeVisibleAsync();
            await Expect(title).ToHaveTextAsync("All Products", new() { IgnoreCase = true });
        }

        [When("I navigate to the Products page")]
        public async Task WhenINavigateToTheProductsPage()
        {
            await _fixture.GetPage().ClickAsync("a:has-text('Products')");
        }

        [When(@"I add a product to the cart")]
        public async Task WhenIAddAProductToTheCart()
        {
            await _fixture.NavigateToUrl();
            var page = _fixture.GetPage();
            await page.Locator("a[data-product-id='1']").First.ClickAsync();
        }

        [When(@"I proceed to checkout")]
        public async Task WhenIProceedToCheckout()
        {
            var page = _fixture.GetPage();
            await page.Locator("text=View Cart").ClickAsync();
            await page.Locator("text=Proceed To Checkout").ClickAsync();
        }

        [Then(@"delivery and billing addresses should match")]
        public async Task ThenDeliveryAndBillingAddressesShouldMatch()
        {
            var page = _fixture.GetPage();
            var deliveryAddressLocator = page.Locator("#address_delivery li");
            var billingAddressLocator = page.Locator("#address_invoice li");

            var deliveryLines = await deliveryAddressLocator.AllInnerTextsAsync();
            var billingLines = await billingAddressLocator.AllInnerTextsAsync();

            bool addressesMatch = deliveryLines.SequenceEqual(billingLines);

            if (addressesMatch)
                Console.WriteLine("Delivery and Billing addresses match.");
            else
                Console.WriteLine("Delivery and Billing addresses do NOT match.");
        }

        [When(@"I place the order with payment details")]
        public async Task WhenIPlaceTheOrderWithPaymentDetails()
        {
            var page = _fixture.GetPage();
            var user = (UserTestData)_scenarioContext["user"];
            await page.Locator("textarea[name='message']").FillAsync("This is a test message.");
            await page.GetByRole(AriaRole.Link, new() { Name = "Place Order" }).ClickAsync();

            await page.Locator("input[data-qa='name-on-card']").FillAsync(user.FirstName);
            await page.Locator("input[data-qa='card-number']").FillAsync("1234 5678 1234 5678");
            await page.Locator("input[data-qa='cvc']").FillAsync("999");
            await page.Locator("input[data-qa='expiry-month']").FillAsync("12");
            await page.Locator("input[data-qa='expiry-year']").FillAsync("2030");

            await page.ClickAsync("#submit");
        }

        [When("I click the Signup button")]
        public async Task WhenIClickTheSignupButton()
        {
            await _fixture.GetPage().Locator("button[data-qa='signup-button']").ClickAsync();
        }

        [Then(@"the order should be confirmed")]
        public async Task ThenTheOrderShouldBeConfirmed()
        {
            var page = _fixture.GetPage();
            await Expect(page.GetByText("Congratulations! Your order has been confirmed!")).ToBeVisibleAsync();
        }

        [Then(@"I should see an error message saying ""(.*)""")]
        public async Task ThenIShouldSeeAnErrorMessageSaying(string expectedMessage)
        {
            var errorMessage = _fixture.GetPage().GetByText(expectedMessage, new() { Exact = true });
            await Expect(errorMessage).ToBeVisibleAsync();
        }
        
        [When("I enter a random email and click subscribe")]
        public async Task WhenIEnterARandomEmailAndClickSubscribe()
        {
            var page = _fixture.GetPage();
            await page.Locator("#susbscribe_email").FillAsync("testEmail@mail.com");
            await page.ClickAsync("#subscribe");
        }
        
        [Then("I should see a subscription success message")]
        public async Task ThenIShouldSeeASubscriptionSuccessMessage()
        {
            var successMessage = _fixture.GetPage().Locator("div.alert-success.alert");
            await Expect(successMessage).ToBeVisibleAsync();
        }
    }
}