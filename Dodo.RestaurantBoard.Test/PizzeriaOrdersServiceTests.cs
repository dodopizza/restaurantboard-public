using Dodo.RestaurantBoard.Site.ViewModels;
using Dodo.RestaurantBoard.Test.DSL;
using Dodo.Tracker.Contracts;
using Xunit;

namespace Dodo.RestaurantBoard.Test
{
    public class PizzeriaOrdersServiceTests
    {
        [Fact]
        public void WhenGetOrderReadinessToStationaryWith2EmptyOrders_ShouldReturnJsonWith2Orders()
        {
            var orderForPupa = Create.ProductionOrder.For("Пупа")
                .WithId(55).WithNumber(3).WithDate(1.JanOf(2018)).Please();
            var orderForLupa = Create.ProductionOrder.For("Лупа")
                .WithId(56).WithNumber(4).WithDate(2.JanOf(2018)).Please();
            
            var trackerClient = Create.TrackerClient.With(orderForPupa).With(orderForLupa).Please();
            var service = Create.PizzeriaOrdersService.WithTrackerClient(trackerClient).Please();
            var controller = Create.BoardsController.WithPizzeriaOrdersService(service).Please();

            var result = controller.GetOrderReadinessToStationary(10).Result.Value as IOrderReadinessResult;

            AssertOrder(orderForLupa, result.ClientOrders[0]);
            AssertOrder(orderForPupa, result.ClientOrders[1]);
        }

        private void AssertOrder(ProductionOrder expected, IClientOrder result)
        {
            Assert.Equal(expected.Id, result.OrderId);
            Assert.Equal(expected.Number, result.OrderNumber);
            Assert.Equal(expected.ClientName, result.ClientName);
            Assert.Equal(expected.ChangeDate.Value.Ticks, result.OrderReadyTimestamp);
        }
    }
}