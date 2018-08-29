using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class RestaurantBoardShould : IClassFixture<TestFixture>
    {
        private readonly HttpClient _client;
        private readonly Mock<ITrackerClient> _stubTrackerClient;

        public RestaurantBoardShould(TestFixture fixture)
        {
            _client = fixture.Client;
            _stubTrackerClient = fixture.StubTrackerClient;
        }

        [Fact]
        public async Task RedirectToOrderReadinessToStationaryFromIndex()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "");

            var response = await _client.SendAsync(request);

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal(new Uri("/Boards/OrdersReadinessToStationary?unitId=29", UriKind.Relative), response.Headers.Location);
        }

        [Fact]
        public async Task X()
        {
            var expectedProductionOrders = new[]
                {
                    new ProductionOrder
                    {
                        Id = 1,
                        ClientName = "John",
                        Number = 10
                    },
                    new ProductionOrder
                    {
                        Id = 2,
                        ClientName = "Jill",
                        Number = 20
                    }
                };

            _stubTrackerClient.Setup(tc => tc.GetOrdersByTypeAsync(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<int>()))
                .ReturnsAsync(expectedProductionOrders);

            var request = new HttpRequestMessage(HttpMethod.Get, "/Boards/GetOrderReadinessToStationary?unitId=29");

            var response = await _client.SendAsync(request);

            var responseMessage = await response.Content.ReadAsStringAsync();

            dynamic returnedJson = JsonConvert.DeserializeObject(responseMessage);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(2, returnedJson.clientOrders.Count);
        }
    }
}
