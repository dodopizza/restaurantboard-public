using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web.Script.Serialization;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Site.ViewModels;
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

            IOrderReadinessResult result = controller.GetOrderReadinessToStationary(10).Result.Value as IOrderReadinessResult;

            Assert.Equal(2, result.ClientOrders.Count);
        }
    }
}