using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace AutoProxy.Services
{
    internal class ProxyService : IHostedService
    {
        private readonly IConfiguration _config;
        private readonly IWebHost _host;

        public ProxyService(IConfiguration config)
        {
            _config = config;

            var builder = CreateWebHostBuilder();
            _host = builder.Build();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _host.StartAsync();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _host.StopAsync();
            return Task.CompletedTask;
        }

        public IWebHostBuilder CreateWebHostBuilder() =>
            WebHost.CreateDefaultBuilder()
                .UseStartup<ProxyServiceStartup>()
                .UseConfiguration(_config)
                .ConfigureKestrel(options =>
                {
                    options.Listen(IPAddress.Parse(_config["BindToAddress"]), Convert.ToInt32(_config["BindToPort"]),
                        (serverOptions) =>
                        {
                            serverOptions.UseHttps();
                        });
                });
    }
}
