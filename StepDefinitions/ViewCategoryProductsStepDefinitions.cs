using AE_extensive_project.TestFixture;
using TechTalk.SpecFlow;

namespace AE_extensive_project.StepDefinitions
{
    [Binding]
    public class ViewCategoryProductsStepDefinitions
    {
        private readonly TestFixtureBase _fixture;

        public ViewCategoryProductsStepDefinitions(ScenarioContext context)
        {
            _fixture = (TestFixtureBase)context["fixture"];
        }

        [When(@"I expand the ""(.*)"" category")]
        public async Task WhenIExpandTheCategory(string category)
        {
            var page = _fixture.GetPage();
            await page.Locator($"div.left-sidebar .panel-group a[href='#{category}']").ClickAsync();
        }

        [When(@"I select the ""(.*)"" subcategory")]
        public async Task WhenISelectTheSubcategory(string subcategory)
        {
            var page = _fixture.GetPage();

            //map subcategories to their parent categories
            var subcategoryToCategoryIdMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Dress", "Women" },
                { "Jeans", "Men" },
                // can add more if needed
            };

            if (!subcategoryToCategoryIdMap.TryGetValue(subcategory, out var parentCategoryId))
                throw new ArgumentException($"No category mapping found for subcategory: {subcategory}");

            var subcategoryLocator = $"#{parentCategoryId} ul li a:has-text('{subcategory}')";
            await page.Locator(subcategoryLocator).ClickAsync();
        }

        [Then(@"I should see products for ""(.*)""")]
        public async Task ThenIShouldSeeProductsFor(string expectedTitle)
        {
            var page = _fixture.GetPage();

            var headingLocator = page.Locator("h2.title.text-center");
            var actualTitle = await headingLocator.InnerTextAsync();

            Assert.That(actualTitle, Does.Contain(expectedTitle).IgnoreCase,
                $"Expected heading to contain '{expectedTitle}', but was '{actualTitle}'");
        }
    }
}