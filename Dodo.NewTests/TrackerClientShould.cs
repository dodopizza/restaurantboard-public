using System;
using System.Linq;
using Dodo.Core.Common;
using Xunit;    
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;

namespace Dodo.NewTests
{
    public class TrackerClientShould
    {
        [Fact]
        public void ReturnOrdersSortedByName()
        {
            var trackerClient = CreateTrackerClientWhichReturnsOrders(new[]
            {
                new ProductionOrder {ClientName = "Пупа"},
                new ProductionOrder {ClientName = "Лупа"}
            });
            
            var orders = trackerClient.GetSortedOrdersByType(Uuid.NewUUId(), OrderType.Delivery, new OrderState[0], 3);

            Assert.Equal("Лупа", orders.First().ClientName);
            Assert.Equal("Пупа", orders.Last().ClientName);
        }
        
        [Fact]
        public void ReturnOnlyOneOrder_IfLimitIsOne()
        {
            var orders = new[]
            {
                new ProductionOrder {ClientName = "Пупа"},
                new ProductionOrder {ClientName = "Лупа"}
            };
            var trackerClient = CreateTrackerClientWhichReturnsOrders(orders);
            
            var limitedOrders = trackerClient.LimitOrders(orders, 1);

            Assert.Equal(1, limitedOrders.Length);
        }

        private TrackerClient CreateTrackerClientWhichReturnsOrders(ProductionOrder[] orders)
        {
            var trackerClientMock = new Mock<TrackerClient> { CallBase = true };
            trackerClientMock
                .Setup(o => o.GetOrdersByType(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<OrderState[]>(), It.IsAny<int>()))
                .Returns(new[]
                {
                    new ProductionOrder { ClientName = "Пупа" },
                    new ProductionOrder { ClientName = "Лупа" }
                });
            return trackerClientMock.Object;
        }
    }
}