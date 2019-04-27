using AutoProxy.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace AutoProxy
{
    class Program
    {
        private static IHost _host;
        private static string _rootPath;

        static async Task Main(string[] args)
        {
            // Explicitly set working directory.
            _rootPath = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory(_rootPath);

            // Build our environment.
            _host = new HostBuilder()
                .UseContentRoot(_rootPath)
                .ConfigureLogging((hostContext, logger) =>
                {
                    // Set logging level
                    logger.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                    logger.AddConsole();
                    logger.AddDebug();
                    logger.AddEventSourceLogger();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    // Add Kestrel as a hosted service.
                    services.AddHostedService<ProxyService>();
                })
                .ConfigureAppConfiguration(hostConfig =>
                {
                    hostConfig.SetBasePath(_rootPath);
                    hostConfig.AddJsonFile("appsettings.json", optional: false);  // Read our configuration file.
                    hostConfig.AddCommandLine(args);
                })
                .UseConsoleLifetime()
                .Build();

            await _host.RunAsync(new CancellationToken());
        }
    }
}
