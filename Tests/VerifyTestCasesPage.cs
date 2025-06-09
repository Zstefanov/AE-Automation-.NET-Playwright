using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    public class VerifyTestCasesPage : TestFixtureBase
    {
        [Test]
        public async Task Verify_Test_Cases_Page()
        {
            await NavigateToUrl();
            //click on 'Test Cases' button
            await Page.GetByRole(AriaRole.Link, new() { Name = "Test Cases", Exact= true}).ClickAsync();

            //verify user is navigated to test cases page successfully
            await Expect(Page.Locator("h2:has-text('Test Cases')")).ToBeVisibleAsync();
        }
    }
}
