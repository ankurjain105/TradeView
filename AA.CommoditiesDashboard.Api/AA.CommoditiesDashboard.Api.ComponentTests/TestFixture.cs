using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace AA.CommoditiesDashboard.Api.ComponentTests
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client { get; }

        protected TestFixture()
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            var webHostBuilder = new WebHostBuilder()
                .ConfigureServices(InitializeServices)
                .UseConfiguration(configurationBuilder.Build())
                .UseEnvironment("Development")
                .UseStartup(typeof(Startup));

            // Create instance of test server
            _server = new TestServer(webHostBuilder);

            // Add configuration for client
            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:5000");
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected virtual void InitializeServices(IServiceCollection services)
        {
            var startupAssembly = typeof(Startup).GetTypeInfo().Assembly;

            var manager = new ApplicationPartManager
            {
                ApplicationParts =
                {
                    new AssemblyPart(startupAssembly)
                },
                FeatureProviders =
                {
                    new ControllerFeatureProvider(),
                    new ViewComponentFeatureProvider()
                }
            };

            services.AddSingleton(manager);
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}