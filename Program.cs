using AE_extensive_project.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AE_extensive_project
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            var services = new ServiceCollection();
            services.AddDbContext<TestDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            // Build the ServiceProvider to resolve dependencies
            var serviceProvider = services.BuildServiceProvider();

            // Example usage of TestDbContext
            using (var context = serviceProvider.GetService<TestDbContext>())
            {
                // Perform database operations
            }
        }
    }
}