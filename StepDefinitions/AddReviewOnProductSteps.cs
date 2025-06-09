using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class AddReviewOnProductSteps
    {
        private readonly TestFixtureBase _fixture;

        public AddReviewOnProductSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I click on any ""(.*)"" link")]
        public async Task WhenIClickOnAnyViewProductLink(string linkText)
        {
            var page = _fixture.GetPage();
            await page.GetByRole(AriaRole.Link, new() { Name = linkText }).Nth(0).ClickAsync();
        }

        [When(@"I fill in the review form with name, email, and comment")]
        public async Task WhenIFillInTheReviewForm()
        {
            var page = _fixture.GetPage();
            await page.Locator("#name").FillAsync("test");
            await page.Locator("#email").FillAsync("test@test.com");
            await page.Locator("#review").FillAsync("This is my review for the product.");
        }

        [When(@"I submit the review")]
        public async Task WhenISubmitTheReview()
        {
            await _fixture.GetPage().Locator("#button-review").ClickAsync();
        }

        [Then(@"I should see a success message saying ""(.*)""")]
        public async Task ThenIShouldSeeASuccessMessage(string expectedMessage)
        {
            var page = _fixture.GetPage();
            await Expect(page.Locator("span", new() { HasTextString = expectedMessage })).ToBeVisibleAsync();
        }
    }
}