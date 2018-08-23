using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Test.DSL;
using Xunit;

namespace Dodo.RestaurantBoard.Test
{
    public class PizzeriaOrdersServiceTests
    {
        [Fact]
        public void WhenGetOrderReadinessToStationaryWith2EmptyOrders_ShouldReturnJsonWith2Orders()
        {
            var trackerClient = Create.TrackerClientBuilder.WithEmptyOrders(2).Please();
            var service = Create.PizzeriaOrdersServiceBuilder.WithTrackerClient(trackerClient).Please();
            var controller = Create.BoardsControllerBuilder.WithPizzeriaOrdersService(service).Please();

            dynamic result = controller.GetOrderReadinessToStationary(10);
            var clientOrder = result.Result.Value.GetType().GetProperty("ClientOrders") as IEnumerable<dynamic>;
            var o = clientOrder.GetType().GetProperty("ClientName").GetValue(clientOrder).ToString();
            
            //var orders = result.Result.Result.Value.ClientOrders.ToList();

            //Assert.Equal(2, orders.Count());
        }
    }
}