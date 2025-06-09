using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;
using static AE_extensive_project.TestData.TestDataGenerator;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class PlaceOrderRegisterBeforeCheckoutStepDefinitions
    {
        private readonly TestFixtureBase _fixture;
        private readonly ScenarioContext _scenarioContext;
        private UserTestData _user;

        public PlaceOrderRegisterBeforeCheckoutStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _fixture = (TestFixtureBase)scenarioContext["fixture"];
        }

        [When(@"I register a new user")]
        public async Task WhenIRegisterANewUser()
        {
            var registerHelper = new Helpers.RegisterUser();
            var baseUrl = _fixture.GetConfiguration()["baseUrl"];

            _user = await registerHelper.RegisterUserHelper(_fixture.GetPage(), baseUrl);
            _scenarioContext["user"] = _user;

            Console.WriteLine($"Registered user: {_user.FirstName} {_user.LastName}, Email: {_user.Email}");

            await _fixture.GetPage().Locator("a[data-qa='continue-button']").ClickAsync();
        }
    }
}