using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class ContactUsFormSteps
    {
        private readonly TestFixtureBase _fixture;

        public ContactUsFormSteps(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I navigate to the Contact Us page")]
        public async Task WhenINavigateToTheContactUsPage()
        {
            var page = _fixture.GetPage();
            await page.ClickAsync("text=Contact us");

            //handle alert popup if it appears
            page.Dialog += async (_, dialog) =>
            {
                TestContext.Out.WriteLine($"Dialog message: {dialog.Message}");
                await dialog.AcceptAsync();
            };
        }

        [When(@"I fill out the contact form with valid details")]
        public async Task WhenIFillOutTheContactFormWithValidDetails()
        {
            var page = _fixture.GetPage();

            //confirm "GET IN TOUCH" section is visible
            await page.Locator("h2:has-text('Get In Touch')").WaitForAsync(new() { Timeout = 10000 });

            await page.Locator("[data-qa='name']").FillAsync("RandomName");
            await page.Locator("[data-qa='email']").FillAsync("test@example.com");
            await page.GetByPlaceholder("Subject").FillAsync("Test Subject");
            await page.GetByPlaceholder("Your Message Here").FillAsync("This is my test message.");
        }

        [When(@"I upload a file")]
        public async Task WhenIUploadAFile()
        {
            var page = _fixture.GetPage();
            var filePath = Path.Combine(AppContext.BaseDirectory, "Resources", "FileForUpload.txt");

            await page.SetInputFilesAsync("input[name='upload_file']", filePath);
        }

        [When(@"I submit the contact form")]
        public async Task WhenISubmitTheContactForm()
        {
            var page = _fixture.GetPage();

            await page.WaitForTimeoutAsync(1000);
            await page.Locator("[data-qa='submit-button']").ClickAsync();

            //wait for the success message specifically inside #contact-page
            var successLocator = page.Locator("#contact-page .alert-success");
            await successLocator.WaitForAsync(new() { Timeout = 10000 });
        }

        [Then(@"I should see the success message ""(.*)""")]
        public async Task ThenIShouldSeeTheSuccessMessage(string expectedMessage)
        {
            var page = _fixture.GetPage();

            var successLocator = page.Locator("#contact-page .alert-success");
            await successLocator.WaitForAsync(new() { Timeout = 10000 });

            await Expect(successLocator).ToHaveTextAsync(expectedMessage);
        }
    }
}