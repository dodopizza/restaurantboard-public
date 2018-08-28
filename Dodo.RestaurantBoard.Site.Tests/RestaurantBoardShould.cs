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

        public RestaurantBoardShould(TestFixture fixture)
        {
            _client = fixture.Client;
        }

        [Fact]
        public async Task RedirectToOrderReadinessToStationaryFromIndex()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "");

            var response = await _client.SendAsync(request);

            var responseMessage = await response.Content.ReadAsStringAsync();

            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
            Assert.Equal(new Uri("/Boards/OrdersReadinessToStationary?unitId=29", UriKind.Relative), response.Headers.Location);
        }
    }
}
