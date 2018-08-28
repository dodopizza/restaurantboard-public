using Dodo.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Net.Http;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;

        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Core.Startup>()
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.SetBasePath(
                        Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "..\\..\\..\\..\\src\\Dodo.RestaurantBoard\\Dodo.RestaurantBoard.Site"));
                    configBuilder.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ITrackerClient, StubTrackerClient>();
                });

            _server = new TestServer(builder);

            Client = _server.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
