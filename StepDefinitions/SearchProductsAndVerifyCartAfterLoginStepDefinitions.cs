using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class SearchProductsAndVerifyCartAfterLoginStepDefinitions
    {
        private readonly TestFixtureBase _fixture;
        private IPage Page => _fixture.GetPage();

        public SearchProductsAndVerifyCartAfterLoginStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [Then(@"I should see search results for ""(.*)""")]
        public async Task ThenIShouldSeeSearchResultsFor(string product)
        {
            var results = Page.Locator("div.productinfo p", new() { HasTextString = product });
            await Expect(results).ToBeVisibleAsync();
        }

        [When(@"I add the product with id ""(.*)"" to the cart")]
        public async Task WhenIAddTheProductWithIdToTheCart(string productId)
        {
            await Page.Locator($"a.add-to-cart[data-product-id='{productId}']:visible").First.ClickAsync();
        }

        [When(@"I log in with valid credentials")]
        public async Task WhenILogInWithValidCredentials()
        {
            string email = _fixture.GetConfiguration()["ValidCredentials:EmailAddress"];
            string password = _fixture.GetConfiguration()["ValidCredentials:Password"];
            await _fixture.Login(email, password);
        }

        [When(@"I go to the Cart page")]
        public async Task WhenIGoToTheCartPage()
        {
            await Page.GetByRole(AriaRole.Link, new() { Name = "Cart" }).ClickAsync();
        }

        [Then(@"I should see ""(.*)"" in the cart")]
        public async Task ThenIShouldSeeInTheCart(string productName)
        {
            var productInCart = Page.Locator("tr", new() { HasTextString = productName });
            await Expect(productInCart).ToBeVisibleAsync();
        }
    }
}