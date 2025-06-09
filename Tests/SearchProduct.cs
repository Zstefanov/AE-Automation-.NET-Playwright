using AE_extensive_project.Helpers;
using AE_extensive_project.TestFixture;
using System.ComponentModel.DataAnnotations;
using static Microsoft.Playwright.Assertions;


namespace AE_extensive_project.Tests
{
    //3 types of data loading examples below
    public class SearchProduct : TestFixtureBase
    {
        //Test with static data provided in TestCases(based on products we know are available)
        [Test]
        [TestCase("Blue Top")]
        [TestCase("Men Tshirt")]
        [TestCase("Lace Top For Women")]
        public async Task TestStaticSearchProduct(string productName)
        {
            await NavigateToUrl();

            //verify home page is visible successfully via unique element
            Assert.That(await Page.Locator("#slider").IsVisibleAsync());

            //click on 'Products' button
            await NavigateToProductsPage();

            //verify user is navigated to ALL PRODUCTS page
            var isOnHomePage = Page.Locator("h2.title:text('All Products')");
            await Expect(isOnHomePage).ToBeVisibleAsync();

            //enter product name in search input and click search button
            await Page.Locator("#search_product").FillAsync(productName);
            await Page.Locator("#submit_search").ClickAsync();

            //verify the ' SEARCHED PRODUCT' text is visible
            var searchResultsHeader = Page.Locator("h2.title:text('Searched Products')");
            await Expect(searchResultsHeader).ToBeVisibleAsync();

            //validate if one of the returned products contains the search term(case-insensitive)
            //.features_items -> product list container
            //.productinfo -> individual product card info
            //p -> name of each individual product
            var matchedProduct = Page.Locator(".features_items .productinfo p");
            var productTexts = await matchedProduct.AllInnerTextsAsync();

            Assert.That(productTexts.Any(name => name.Contains(productName, StringComparison.OrdinalIgnoreCase)),
                    $"Search results do not contain expected product: {productName}");
        }

        //Test with dynamic data taken straight from the product list items available on the page
        [Test]
        public async Task TestSearchForRandomVisibleProduct()
        {
            await NavigateToUrl();
            await NavigateToProductsPage();

            //grab a random product name from the page
            var productNames = await Page.Locator(".productinfo p").AllInnerTextsAsync();

            var random = new Random();
            var productToSearch = productNames[random.Next(productNames.Count)];

            //perform the search
            await Page.FillAsync("input[name='search']", productToSearch);
            await Page.ClickAsync("button[type='submit']");

            //verify search result
            var resultProduct = Page.Locator(".productinfo p", new() { HasTextString = productToSearch });
            Assert.That(await resultProduct.IsVisibleAsync(), $"Search did not return expected product: {productToSearch}");
        }


        //Data-Driven approach using TestLoaderHelper class, loading from SearchProducts.json
        //perfect for scalability 
        [Test, TestCaseSource(typeof(TestDataLoaderHelper), nameof(TestDataLoaderHelper.LoadSearchProductTestCases))]
        public async Task TestDataDrivenSearchProduct(SearchProductTestCase testCase)
        {
            await NavigateToUrl();
            await NavigateToProductsPage();

            //perform search
            await Page.FillAsync("input[name='search']", testCase.SearchTerm);
            await Page.ClickAsync("button[type='submit']");

            var productLocator = Page.Locator(".features_items .productinfo p");
            var allResults = await productLocator.AllInnerTextsAsync();

            bool foundMatch = allResults.Any(p =>
                p.Contains(testCase.SearchTerm, StringComparison.OrdinalIgnoreCase));

            if (testCase.ExpectedResult)
            {
                Assert.That(foundMatch, $"Expected to find '{testCase.SearchTerm}' but didn't.");
            }
            else
            {
                Assert.That(foundMatch,Is.False, $"Expected NOT to find '{testCase.SearchTerm}' but did.");
            }
        }
    }
}
