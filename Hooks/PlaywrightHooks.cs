using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;


namespace AE_extensive_project.Hooks
{
      [Binding]
    public class PlaywrightHooks
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestFixtureBase _fixture;

        public PlaywrightHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _fixture = new TestFixtureBase(); // Instantiate manually
            _scenarioContext["fixture"] = _fixture;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            await _fixture.Setup();
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            await _fixture.TearDown();
        }
    }
}
