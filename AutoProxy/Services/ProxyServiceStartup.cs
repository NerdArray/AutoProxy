using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;

namespace AutoProxy.Services
{
    internal class ProxyServiceStartup
    {
        private IConfiguration _config;
        private ILogger<ProxyServiceStartup> _logger;

        public ProxyServiceStartup(
            IConfiguration config,
            ILogger<ProxyServiceStartup> logger)
        {
            _config = config;
            _logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add logging.
            services.AddLogging();

            // Add a named HttpClient with a handler
            // to cache Autotask credentials in the
            // auth header.
            services
                .AddHttpClient("Autotask")
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var baseUri = new Uri(_config["AutotaskBaseUrl"]);

                    CredentialCache creds = new CredentialCache();
                    creds.Add(baseUri, "basic", new NetworkCredential(_config["ApiUsername"], _config["ApiPassword"]));

                    var handler = new HttpClientHandler();
                    handler.Credentials = creds;

                    return handler;
                });

            // Add the Autotask service
            services.AddSingleton<AutotaskService>();

            // Set a very short HSTS age.
            services.AddHsts(options =>
            {
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromMinutes(3);
                options.ExcludedHosts.Add("localhost");
            });

            // Add our MVC Web API.
            services.AddMvcCore(options =>
                {
                    options.RequireHttpsPermanent = true;
                    options.EnableEndpointRouting = true;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddCors();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                // TODO: log request
                await next.Invoke();
            });

            app.UseHttpsRedirection();
            app.Map("/api", ApiHandler);
        }

        private void ApiHandler(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}