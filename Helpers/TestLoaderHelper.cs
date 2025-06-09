using System.Text.Json;


namespace AE_extensive_project.Helpers
{
    public static class TestDataLoaderHelper
    {
        public static IEnumerable<SearchProductTestCase> LoadSearchProductTestCases()
        {
            var jsonPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Resources", "SearchProducts.json");
            var jsonData = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<List<SearchProductTestCase>>(jsonData);
        }
    }
}