using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class VerifyTestCasesStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public VerifyTestCasesStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When("I click the Test Cases link")]
        public async Task WhenIClickTheTestCasesLink()
        {
            await _fixture.GetPage().GetByRole(AriaRole.Link, new() { Name = "Test Cases", Exact = true }).ClickAsync();
        }

        [Then("I should be on the Test Cases page")]
        public async Task ThenIShouldBeOnTheTestCasesPage()
        {
            await Expect(_fixture.GetPage().Locator("h2:has-text('Test Cases')")).ToBeVisibleAsync();
        }
    }
}