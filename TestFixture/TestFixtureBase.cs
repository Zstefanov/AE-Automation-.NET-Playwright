using AE_extensive_project.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using static AE_extensive_project.TestData.TestDataGenerator;

namespace AE_extensive_project.TestFixture
{
    public interface ITestFixtureBase 
    {
        Task NavigateToAutomationExcercisePage();
        Task TakeScreenshotAsync(string fileName);
    }
    public class TestFixtureBase
    {
        protected string Username;
        protected string Password;
        protected string EmailAddress;
        protected IBrowser Browser { get; private set; }
        protected IBrowserContext Context { get; private set; }
        protected IPage Page { get; private set; }
        protected IPlaywright PlaywrightInstance { get; private set; }
        protected static IConfigurationRoot Configuration { get; set; }

        public string baseUrl = "https://automationexercise.com";

        //documentation on : playwright.dev/dotnet/docs/api-testing
        protected IAPIRequestContext ApiContext;

        protected TestSettings testSettings;

        //public getters for specflow due to not directly inheriting the fixturebase class
        public IPage GetPage() => Page;
        public UserTestData TestUser { get; set; }
        public IConfigurationRoot GetConfiguration() => Configuration;

        [SetUp]
        public async Task Setup()
        {
            //config loader
            Configuration = ConfigLoader.LoadConfiguration();

            //load test settings configuration with json file appsettings for browser
            testSettings = ConfigLoader.LoadTestSettings();

            Console.WriteLine($"[DEBUG] ENV BROWSER: {Environment.GetEnvironmentVariable("BROWSER")}");
            PlaywrightInstance = await Playwright.CreateAsync();
            ApiContext = await PlaywrightInstance.APIRequest.NewContextAsync();

            //new browser instance with parameterization
            IBrowserType browserType = PlaywrightInstance.Chromium;

            BrowserTypeLaunchOptions launchOptions = new()
            {
                Headless = testSettings.Headless,
                SlowMo = testSettings.SlowMo,
                Args = new[] { "--start-maximized" }
            };

            //implement different browser options
            switch (testSettings.DriverType)
            {
                case DriverType.Chromium:
                    browserType = PlaywrightInstance.Chromium;
                    break;
                case DriverType.Firefox:
                    browserType = PlaywrightInstance.Firefox;
                    break;
                case DriverType.Edge:
                    browserType = PlaywrightInstance.Chromium;
                    launchOptions.Channel = "msedge";
                    break;
                case DriverType.Chrome:
                    browserType = PlaywrightInstance.Chromium;
                    launchOptions.Channel = "chrome";
                    break;
                case DriverType.WebKit:
                    browserType = PlaywrightInstance.Webkit;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Browser = await browserType.LaunchAsync(launchOptions);

            Context = await Browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = ViewportSize.NoViewport,
                IgnoreHTTPSErrors = true
            });

            Page = await Context.NewPageAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            await Browser.CloseAsync();
            PlaywrightInstance.Dispose();
        }

        public async Task NavigateToUrl()
        {
            //navigate and accept cookies message
            await Page.GotoAsync(Configuration["ApplicationUrl"]);

            var consentButton = Page.GetByRole(AriaRole.Button, new() { Name = "Consent" });

            if (await consentButton.IsVisibleAsync())
            {
                await consentButton.ClickAsync();
            }
        }
        public async Task NavigateToProductsPage()
        {
            //navigate to products page for reusability
            await Page.GetByRole(AriaRole.Link, new() { Name = "Products" }).ClickAsync();
        }
        
        public async Task Login(string email, string password)
        {
            password = Configuration["ValidCredentials:Password"];
            email = Configuration["ValidCredentials:EmailAddress"];

            await NavigateToUrl();
            await Page.GetByText("Signup / Login").ClickAsync();

            await Page.Locator("[data-qa='login-email']").FillAsync(email);
            await Page.Locator("[data-qa='login-password']").FillAsync(password);
            await Page.Locator("[data-qa='login-button']").ClickAsync();
        }
        public async Task Logout()
        {
            //text-based
            await Page.ClickAsync("text=Logout");
        }
    }
}
