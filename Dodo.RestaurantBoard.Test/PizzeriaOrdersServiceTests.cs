using System.Collections.Generic;
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
            var orderForPupa = Create.ProductionOrder.For("Пупа")
                .WithId(55).WithNumber(3).WithDate(1.JanOf(2018)).Please();
            var orderForLupa = Create.ProductionOrder.For("Лупа")
                .WithId(56).WithNumber(4).WithDate(2.JanOf(2018)).Please();
            
            var trackerClient = Create.TrackerClient.With(orderForPupa).With(orderForLupa).Please();
            var service = Create.PizzeriaOrdersService.WithTrackerClient(trackerClient).Please();
            var controller = Create.BoardsController.WithPizzeriaOrdersService(service).Please();

            var result = controller.GetOrderReadinessToStationary(10).Result.Value as IOrderReadinessResult;

            Assert.Equal(new OrderReadinessResult
            {
                PlayTune = 1,
                NewOrderArrived = true,
                SongName = "I will always love you",
                ClientOrders = new List<IClientOrder>
                {
                    new ClientOrder
                    {
                        OrderId = 56,
                        OrderNumber = 4,
                        ClientName = "Лупа",
                        ClientIconPath = null,
                        OrderReadyTimestamp = 2.JanOf(2018).Ticks,
                        OrderReadyDateTime = "02.01.2018 0:00:00"
                    },
                    new ClientOrder
                    {
                        OrderId = 55,
                        OrderNumber = 3,
                        ClientName = "Пупа",
                        ClientIconPath = null,
                        OrderReadyTimestamp = 1.JanOf(2018).Ticks,
                        OrderReadyDateTime = "01.01.2018 0:00:00"
                    }
                }
            }, result);
        }
    }
}