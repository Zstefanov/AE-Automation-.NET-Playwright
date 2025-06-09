using Microsoft.Extensions.Configuration;

namespace AE_extensive_project.Config
{
    public static class ConfigLoader
    {
        //reader of settings in the appsettings file, implemented in TestFixtureBase
        public static IConfigurationRoot LoadConfiguration()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return config.Build();
        }

        public static TestSettings LoadTestSettings()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var settings =  config.Get<TestSettings>();

            // override DriverType from environment variable if set
            //implemented for CL e.g. set BROWSER=Firefox / dotnet test
            string envBrowser = Environment.GetEnvironmentVariable("BROWSER");

            if (!string.IsNullOrEmpty(envBrowser) &&
                    Enum.TryParse(envBrowser, ignoreCase: true, out DriverType parsedDriver))
            {
                settings.DriverType = parsedDriver;  // ✅ OVERRIDE config
                Console.WriteLine($"[CONFIG] Overriding browser from environment: {parsedDriver}");
            }
            else
            {
                Console.WriteLine($"[CONFIG] Using browser from appsettings: {settings.DriverType}");
            }

            return settings;
        }
    }
}
