using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Moq;
using NUnit.Framework;
using Dodo.Core.Common;
using System.Collections.Generic;
using System.Linq;

namespace Dodo.RestaurantBoard.Domain.Tests
{
    public class TrackerClientTests
    {
        [Test]
        public void GetOrdersByType_ReturnsEmptyArray_WhenRepositoryIsEmpty()
        {
            var stubRepository = CreateEmptyRepository();
            var trackerClient = new TrackerClient(stubRepository);

            var orders = GetOrders(trackerClient);

            Assert.IsEmpty(orders);
        }

        [Test]
        public void GetOrdersByType_ReturnsOrdersUpToTheLimit_WhenOrdersRepositoryCountExceedsLimit()
        {
            int ordersLimit = 42;
            var stubRepository = CreateRepository(ordersLimit + 1);
            var trackerClient = new TrackerClient(stubRepository);
            
            var orders = GetOrders(trackerClient, ordersLimit);

            Assert.AreEqual(ordersLimit, orders.Length);
        }

        [Test]
        public void GetOrdersByType_ReturnsOrdersUpToTheRepositoryCount_WhenLimitExceedsRepositoryCount()
        {
            int ordersLimit = 42;
            int repositoryCount = ordersLimit - 1;
            var stubRepository = CreateRepository(repositoryCount);
            var trackerClient = new TrackerClient(stubRepository);

            var orders = GetOrders(trackerClient, ordersLimit);

            Assert.AreEqual(repositoryCount, orders.Length);
        }

        [Test]
        public void GetOrdersByType_CallsRepositoryGetOrders_Once()
        {
            var mockRepository = new Mock<IOrdersRepository>();
            var trackerClient = new TrackerClient(mockRepository.Object);

            var orders = GetOrders(trackerClient);

            mockRepository.Verify(x => x.GetOrders(), Times.Once());
        }

        private static ProductionOrder[] GetOrders(TrackerClient trackerClient, int limit = 42)
        {
            return trackerClient.GetOrdersByType(Uuid.Empty, Tracker.Contracts.Enums.OrderType.Stationary, new Tracker.Contracts.Enums.OrderState[] { }, limit);
        }

        IOrdersRepository CreateEmptyRepository()
        {
            var result = new Mock<IOrdersRepository>();
            result.Setup(x => x.GetOrders()).Returns(new ProductionOrder[0]);
            return result.Object;
        }

        IOrdersRepository CreateRepository(int ordersCount)
        {
            ProductionOrder[] orders = CreateOrders(ordersCount).ToArray();

            var result = new Mock<IOrdersRepository>();
            result.Setup(x => x.GetOrders()).Returns(orders);
            return result.Object;
        }

        private static IEnumerable<ProductionOrder> CreateOrders(int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return new ProductionOrder();
            }
        }
    }
}
