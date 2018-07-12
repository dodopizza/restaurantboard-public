using System;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Moq;
using NUnit.Framework;
using Dodo.Core.Common;

namespace Dodo.RestaurantBoard.Domain.Tests
{
    public class TrackerClientTests
    {
        [Test]
        public void GetOrdersByType_ReturnsEmptyArray_WhenRepositoryIsEmpty()
        {
            var mockRepository = CreateEmptyRepository();
            var trackerClient = new TrackerClient(mockRepository);

            var orders = GetOrders(trackerClient);

            Assert.IsEmpty(orders);
        }

        private static ProductionOrder[] GetOrders(TrackerClient trackerClient)
        {
            return trackerClient.GetOrdersByType(Uuid.Empty, Tracker.Contracts.Enums.OrderType.Stationary, new Tracker.Contracts.Enums.OrderState[] { }, 16);
        }

        IOrdersRepository CreateEmptyRepository()
        {
            var result = new Mock<IOrdersRepository>();
            result.Setup(x => x.GetOrders()).Returns(new ProductionOrder[0]);
            return result.Object;
        }

        
    }
}
