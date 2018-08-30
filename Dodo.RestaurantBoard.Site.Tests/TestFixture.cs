using Dodo.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.IO;
using System.Net.Http;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;

        private readonly Mock<ITrackerClient> _stubTrackerClient = new Mock<ITrackerClient>();

        public HttpClient Client { get; }

        public Mock<ITrackerClient> StubTrackerClient => _stubTrackerClient;

        public TestFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Core.Startup>()
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.SetBasePath(
                        Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "../../../../src/Dodo.RestaurantBoard/Dodo.RestaurantBoard.Site"));
                    configBuilder.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton(_stubTrackerClient.Object);
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
