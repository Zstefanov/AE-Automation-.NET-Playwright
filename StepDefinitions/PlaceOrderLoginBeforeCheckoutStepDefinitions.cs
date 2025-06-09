using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class PlaceOrderLoginBeforeCheckoutStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public PlaceOrderLoginBeforeCheckoutStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [Given(@"I log in with valid credentials")]
        public async Task GivenILogInWithValidCredentials()
        {
            var email = _fixture.GetConfiguration()["ValidCredentials:EmailAddress"];
            var password = _fixture.GetConfiguration()["ValidCredentials:Password"];
            var userName = _fixture.GetConfiguration()["ValidCredentials:UserName"];

            await _fixture.GetPage().GetByRole(AriaRole.Link, new() { Name = "Signup / Login" }).ClickAsync();
            await _fixture.GetPage().Locator("[data-qa='login-email']").FillAsync(email);
            await _fixture.GetPage().Locator("[data-qa='login-password']").FillAsync(password);
            await _fixture.GetPage().Locator("[data-qa='login-button']").ClickAsync();

            var userDisplayLocator = _fixture.GetPage().Locator("a:has(i.fa-user)");
            await Expect(userDisplayLocator).ToContainTextAsync($"Logged in as {userName}");
        }

        [When(@"I place the order with saved user payment details")]
        public async Task WhenIPlaceTheOrderWithSavedUserPaymentDetails()
        {
            var config = _fixture.GetConfiguration();
            var page = _fixture.GetPage();

            await page.Locator("textarea[name='message']").FillAsync("This is a test message.");
            await page.GetByRole(AriaRole.Link, new() { Name = "Place Order" }).ClickAsync();

            await page.Locator("input[data-qa='name-on-card']").FillAsync(config["ValidCredentials:UserName"]);
            await page.Locator("input[data-qa='card-number']").FillAsync("1234 5678 1234 5678");
            await page.Locator("input[data-qa='cvc']").FillAsync("999");
            await page.Locator("input[data-qa='expiry-month']").FillAsync("12");
            await page.Locator("input[data-qa='expiry-year']").FillAsync("2030");

            await page.GetByRole(AriaRole.Button, new() { Name = "Pay and Confirm Order" }).ClickAsync();
        }
    }
}