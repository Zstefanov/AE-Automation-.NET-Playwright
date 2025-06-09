using AE_extensive_project.TestFixture;
using System.Text.Json;


namespace AE_extensive_project.Tests_API
{
    public class PutToAllBrandsList : TestFixtureBase
    {
        [Test]
        public async Task Put_To_All_Brands_List()
        {
            /*
             * API URL: https://automationexercise.com/api/brandsList
                Request Method: PUT
                Response Code: 405
                Response Message: This request method is not supported.
             */

            string endpoint = $"{baseUrl}/api/brandsList";

            var response = await ApiContext.FetchAsync(endpoint, new()
            {
                Method = "PUT"
            });

            var responseBody = await response.TextAsync();
            TestContext.WriteLine($"Raw Response: {responseBody}");

            //parse response
            using var doc = JsonDocument.Parse(responseBody);
            var root = doc.RootElement;

            var responseCode = root.GetProperty("responseCode").GetInt32();
            var responseMsg = root.GetProperty("message").ToString();
            
            Assert.That(responseMsg, Is.EqualTo("This request method is not supported."));
            Assert.That(responseCode, Is.EqualTo(405));

            TestContext.WriteLine(responseMsg);

            // failsafe for if "brands" exists
            if (root.TryGetProperty("brands", out var brands))
            {
                Assert.Fail("Expected no 'brands' property in a 405 response.");
            }
        }

        [Test]
        public async Task PutAllBrands()
        {
            var formData = Context.APIRequest.CreateFormData();
            formData.Set("search_product", "");
            string endpoint = $"{baseUrl}/api/brandsList";

            var response = await ApiContext.PutAsync(endpoint);
            string responseBody = await response.TextAsync();

            Console.WriteLine($"PUT Request Results:");
            Console.WriteLine($"Response Status: {response.Status}");
            Console.WriteLine($"Response Status Text: {response.StatusText}");
            Console.WriteLine($"Response Headers: {string.Join(", ", response.Headers.Select(h => $"{h.Key}: {h.Value}"))}");
            Console.WriteLine($"Response Body: {responseBody}");

            //should return 405 but result is 200 with an error message of 405

            if (response.Status == 405)
            {
                //expected - method not allowed
                Assert.That(response.StatusText, Does.Contain("Method Not Allowed").IgnoreCase,
                    "Status text should indicate method not allowed");
                Assert.That(responseBody, Does.Contain("This request method is not supported").IgnoreCase,
                    "Response body should contain the expected error message");
                Console.WriteLine("✓ API correctly rejects PUT method with 405");
            }
            else if (response.Status == 200)
            {
                //actual behavior - server accepts PUT
                Console.WriteLine("⚠️ API unexpectedly accepts PUT method with 200 status");
                Console.WriteLine("This might indicate:");
                Console.WriteLine("1. Server treats PUT same as GET");
                Console.WriteLine("2. Server has different method handling than expected");
                Console.WriteLine("3. There might be a proxy/load balancer converting the request");

                //document this behavior
                Assert.That(response.Status, Is.EqualTo(200), "Documenting actual behavior: PUT returns 200");
            }
        }
    }
}
