using AE_extensive_project.TestFixture;


namespace AE_extensive_project.Tests
{
    public class RegisterUser : TestFixtureBase
    {
        [Test]
        public async Task Register_User()
        {
            await NavigateToUrl();
            var registerHelper = new Helpers.RegisterUser();
            await registerHelper.RegisterUserHelper(Page, baseUrl);
        }
        //[Test]
        //public async Task OldReg()
        //{
        //    //var registerHelper = new RegisterUser();
        //    //await registerHelper.RegisterUserHelper();

        //    //generate fake user data from helper class
        //    var user = TestDataGenerator.GenerateUser();
        //    //from base class
        //    await NavigateToUrl();

        //    await Expect(Page.GetByText("Signup / Login")).ToBeVisibleAsync();
        //    await Page.GetByText("Signup / Login").ClickAsync();

        //    var userName = Page.Locator("input[data-qa='signup-name']");
        //    await userName.FillAsync(user.FirstName); //faker name

        //    var userEmail = Page.Locator("input[data-qa='signup-email']");
        //    await userEmail.FillAsync(user.Email); //faker email

        //    var signupBtn = Page.Locator("button[data-qa='signup-button']");
        //    await signupBtn.ClickAsync();

        //    //enter user details
        //    await Page.Locator("#id_gender1").ClickAsync();
        //    await Page.Locator("#password").FillAsync(user.Password); //faker password

        //    await Page.Locator("#days").SelectOptionAsync("10");
        //    await Page.Locator("#months").SelectOptionAsync("5");
        //    await Page.Locator("#years").SelectOptionAsync("1990");

        //    await Page.Locator("#newsletter").SetCheckedAsync(true);
        //    await Page.Locator("#optin").SetCheckedAsync(true);

        //    await Page.Locator("#first_name").FillAsync(user.FirstName);
        //    await Page.Locator("#last_name").FillAsync(user.LastName);
        //    await Page.Locator("#company").FillAsync(user.Company);
        //    await Page.Locator("#address1").FillAsync("3227 Melody Lane");
        //    await Page.Locator("#address2").FillAsync("3227 Melody Lane");
        //    await Page.Locator("#country").SelectOptionAsync("United States");
        //    await Page.Locator("#state").FillAsync("Virginia");
        //    await Page.Locator("#city").FillAsync("Richmond");
        //    await Page.Locator("#zipcode").FillAsync("23173");
        //    await Page.Locator("#mobile_number").FillAsync(user.MobileNumber);

        //    //click create account
        //    await Page.Locator("button[data-qa='create-account']").ClickAsync();

        //    //validated created successfully message
        //    var confirmationText = Page.GetByText("Account Created!");

        //    await Expect(confirmationText).ToContainTextAsync("Account Created");
        //    await Expect(confirmationText).ToBeVisibleAsync();
        //}
    }
}
