using AE_extensive_project.TestData;
using Microsoft.Playwright;
using static AE_extensive_project.TestData.TestDataGenerator;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Helpers
{
    public class RegisterUser
    {
        public async Task<UserTestData> RegisterUserHelper(IPage page, string baseUrl)
        {
            //generate fake user data from helper class
            var user = TestDataGenerator.GenerateUser();

            // Click signup/login
            await Expect(page.GetByText("Signup / Login")).ToBeVisibleAsync();
            await page.GetByText("Signup / Login").ClickAsync();

            // Fill name/email
            var userName = page.Locator("input[data-qa='signup-name']");
            await userName.FillAsync(user.FirstName); //faker name

            var userEmail = page.Locator("input[data-qa='signup-email']");
            await userEmail.FillAsync(user.Email); //faker email

            await page.Locator("button[data-qa='signup-button']").ClickAsync();

            //enter user details
            await page.Locator("#id_gender1").ClickAsync();
            await page.Locator("#password").FillAsync(user.Password); //faker password

            await page.Locator("#days").SelectOptionAsync("10");
            await page.Locator("#months").SelectOptionAsync("5");
            await page.Locator("#years").SelectOptionAsync("1990");

            await page.Locator("#newsletter").SetCheckedAsync(true);
            await page.Locator("#optin").SetCheckedAsync(true);

            await page.Locator("#first_name").FillAsync(user.FirstName);
            await page.Locator("#last_name").FillAsync(user.LastName);
            await page.Locator("#company").FillAsync(user.Company);
            await page.Locator("#address1").FillAsync("3227 Melody Lane");
            await page.Locator("#address2").FillAsync("3227 Melody Lane");
            await page.Locator("#country").SelectOptionAsync("United States");
            await page.Locator("#state").FillAsync("Virginia");
            await page.Locator("#city").FillAsync("Richmond");
            await page.Locator("#zipcode").FillAsync("23173");
            await page.Locator("#mobile_number").FillAsync(user.MobileNumber);

            //click create account
            await page.Locator("button[data-qa='create-account']").ClickAsync();

            //validated created successfully message
            var confirmationText = page.GetByText("Account Created!");

            await Expect(confirmationText).ToContainTextAsync("Account Created");
            await Expect(confirmationText).ToBeVisibleAsync();

            return user;
        }
    }
}
