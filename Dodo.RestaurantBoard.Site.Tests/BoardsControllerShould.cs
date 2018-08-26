using System.Linq;
using System.Threading.Tasks;
using Dodo.RestaurantBoard.Site.Models;
using Dodo.RestaurantBoard.Site.Tests.DSL;
using Dodo.Tracker.Contracts;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class BoardsControllerShould
    {
        [Fact]
        public async Task ReturnOrder_PassedToTrackerClient()
        {
            var orders = new[]
            {
                new ProductionOrder
                {
                    Id = 55,
                    Number = 3,
                    ClientName = "Ivan"
                },
            };

            var trackerClient = Create
                .TrackerClient
                .WithProductionOrders(orders)
                .Please();
            var trackerService = Create
                .TrackerService
                .WithTrackerClient(trackerClient)
                .Please();
            var boardsController = Create
                .BoardController
                .WithTrackerService(trackerService)
                .Please();

            var result = await boardsController.GetOrderReadinessToStationary(55);

            var orderReadiness = result.Value as OrderReadiness;
            Assert.Equal("Ivan", orderReadiness.ClientOrders.Single().ClientName);
            Assert.Equal(3, orderReadiness.ClientOrders.Single().OrderNumber);
            Assert.Equal(55, orderReadiness.ClientOrders.Single().OrderId);
        }
    }
}