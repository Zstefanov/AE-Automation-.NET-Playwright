using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using TechTalk.SpecFlow;
using static AE_extensive_project.TestData.TestDataGenerator;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class DownloadInvoiceAfterPurchaseSteps
    {
        private readonly TestFixtureBase _fixture;
        //private UserTestData user;
        private IDownload download;

        public DownloadInvoiceAfterPurchaseSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I navigate to the registration page")]
        public async Task WhenINavigateToTheRegistrationPage()
        {
            await _fixture.GetPage().GetByRole(AriaRole.Link, new() { Name = "Register / Login" }).ClickAsync();
        }

        [Then(@"I should be able to download the invoice")]
        public async Task ThenIShouldBeAbleToDownloadTheInvoice()
        {
            var page = _fixture.GetPage();

            var downloadTask = page.RunAndWaitForDownloadAsync(async () =>
            {
                await page.Locator("a.btn.check_out", new() { HasTextString = "Download Invoice" }).ClickAsync();
            });

            download = await downloadTask;
            var fileName = download.SuggestedFilename;

            Console.WriteLine($"Downloaded file: {fileName}");

            var downloadPath = Path.Combine("downloads", fileName);
            await download.SaveAsAsync(downloadPath);

            bool fileExists = File.Exists(downloadPath);
            Assert.That(fileExists, Is.True, $"Expected download file '{fileName}' does not exist.");
        }

        [Then(@"I should see ""(.*)"" confirmation after deleting my account")]
        public async Task ThenIShouldSeeConfirmationAfterDeletingMyAccount(string confirmationMessage)
        {
            var page = _fixture.GetPage();

            //click 'Continue' button after invoice
            await page.Locator("[data-qa='continue-button']").ClickAsync();

            //click 'Delete Account' button
            await page.Locator("a[href='/delete_account']").ClickAsync();

            //verify confirmation message
            await Expect(page.GetByText(confirmationMessage)).ToBeVisibleAsync();

            //click final continue button
            await page.ClickAsync("[data-qa='continue-button']");
        }
    }
}