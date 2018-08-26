using Dodo.RestaurantBoard.Site.ViewModels;
using Dodo.RestaurantBoard.Test.DSL;
using Dodo.Tracker.Contracts;
using Xunit;

namespace Dodo.RestaurantBoard.Test
{
    public class BoardsControllerTests
    {
        [Fact]
        public void WhenGetOrderReadinessToStationaryWith2Orders_ShouldReturnOrderReadinessResultWith2CorrectOrders()
        {
            var orderForPupa = Create.ProductionOrder.For("Пупа")
                .WithId(55).WithNumber(3).WithDate(1.JanOf(2018)).Please();
            var orderForLupa = Create.ProductionOrder.For("Лупа")
                .WithId(56).WithNumber(4).WithDate(2.JanOf(2018)).Please();
            
            var trackerClient = Create.TrackerClient.With(orderForPupa).With(orderForLupa).Please();
            var service = Create.PizzeriaOrdersService.WithTrackerClient(trackerClient).Please();
            var controller = Create.BoardsController.WithPizzeriaOrdersService(service).Please();

            var result = controller.GetOrderReadinessToStationary(10).Result.Value as IOrderReadinessResult;

            Assert.Equal(2, result.ClientOrders.Count);
            AssertOrderEquals(orderForLupa, result.ClientOrders[0]);
            AssertOrderEquals(orderForPupa, result.ClientOrders[1]);
        }

        private void AssertOrderEquals(ProductionOrder expected, IClientOrder actual)
        {
            Assert.Equal(expected.Id, actual.OrderId);
            Assert.Equal(expected.Number, actual.OrderNumber);
            Assert.Equal(expected.ClientName, actual.ClientName);
            Assert.Equal(expected.ChangeDate.Value.Ticks, actual.OrderReadyTimestamp);
        }
    }
}