using Dodo.RestaurantBoard.Site.Tests.DSL;
using Dodo.RestaurantBoard.Site.Tests.Extentions;
using Dodo.Tracker.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class BoardControllerTests
    {
        [Fact]
        public async Task GetOrderReadinessToStationary_ReturnsOrdersFromTrackerClient()
        {
            var productionOrders = new[]
            {
                new ProductionOrder
                {
                    ClientName = "Buba"
                }
            };
            var trackerClient = Create.TrackerClientStub(productionOrders).Object;
            var boardContoller = Create.BoardController.WithTrackerClient(trackerClient).Build();

            var jsonResult = await boardContoller.GetOrderReadinessToStationary(42);
            var orders = jsonResult.Value.GetValue<IEnumerable<object>>("ClientOrders").ToList();

            Assert.Equal("Buba", orders.Single().GetValue<string>("ClientName"));
        }
    }
}
