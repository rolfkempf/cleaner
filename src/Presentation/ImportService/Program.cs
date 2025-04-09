using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace ImportService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Build a host with dependency injection support
            using var host = CreateHostBuilder(args).Build();

            // Perform import operations here
            System.Console.WriteLine("Import Service started");

            // Wait for the host to terminate
            await host.RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    // Register services for DI
                    // services.AddScoped<IImportService, ImportService>();

                    // Add any additional configuration here
                });
    }
}
