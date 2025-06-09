using AE_extensive_project.TestFixture;
using Microsoft.Playwright;
using static AE_extensive_project.Helpers.FileUploadHelper;
using static Microsoft.Playwright.Assertions;

namespace AE_extensive_project.Tests
{
    public class ContactUsForm : TestFixtureBase
    {
        [Test]
        public async Task Contact_Us_Form()
        {
            //home page
            await NavigateToUrl();

            //navigate to 'Contact Us'
            await Page.ClickAsync("text=Contact us");

            
            //opening a dialog to handle the browser popup
            Page.Dialog += async (_, dialog) =>
            {
                TestContext.Out.WriteLine($"Dialog message: {dialog.Message}");
                await dialog.AcceptAsync();
            };

            //verify 'GET IN TOUCH' is visible
            await  Page.Locator("a[href='/contact_us']:has-text('Contact us')").IsVisibleAsync();
            
            //enter name, email, subject and message
            await Page.Locator("[data-qa='name']").FillAsync("RandomName");
            await Page.Locator("[data-qa='email']").FillAsync("test@example.com");
            await Page.GetByPlaceholder("Subject").FillAsync("Test Subject");
            await Page.GetByPlaceholder("Your Message Here").FillAsync("This is my test message.");

            //upload file from Resources 
            string filePath = Path.Combine(AppContext.BaseDirectory, "Resources", "FileForUpload.txt");
            await UploadFileAsynch(Page, "input[name='upload_file']", filePath);

            await Page.WaitForTimeoutAsync(2000);
            //click 'Submit' button and wait(test fails in headless mode)
            await Page.Locator("[data-qa='submit-button']").ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            //optional timeout so the message can appear 
            await Page.WaitForTimeoutAsync(2000);

            //locate confirmation message
            var successLocator = Page.GetByText("Success! Your details have been submitted successfully.", new() { Exact = true }).Nth(0);

            //explicit timeout due to fails in headless mode(longer load for some objects)
            await successLocator.WaitForAsync(new() { Timeout = 10000 });
            string formConfirmationText = "Success! Your details have been submitted successfully.";

            await Expect(successLocator).ToHaveTextAsync(formConfirmationText);
        }
    }
}
