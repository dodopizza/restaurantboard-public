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
            var stubRepository = CreateEmptyRepository();
            var trackerClient = new TrackerClient(stubRepository);

            var orders = GetOrders(trackerClient);

            Assert.IsEmpty(orders);
        }

        [Test]
        public void GetOrdersByType_ReturnsOrdersUpToTheLimit_WhenOrdersRepositoryCountExceedsLimit()
        {
            var stubRepository = CreateRepository();
            var trackerClient = new TrackerClient(stubRepository);
            int ordersLimit = 1;
            
            var orders = GetOrders(trackerClient, ordersLimit);

            Assert.AreEqual(ordersLimit, orders.Length);
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

        IOrdersRepository CreateRepository()
        {
            var orders = new[]
            {
                new ProductionOrder
                {
                    Id = 55,
                    Number = 3,
                    ClientName = "Пупа"
                },
                new ProductionOrder
                {
                    Id = 56,
                    Number = 4,
                    ClientName = "Лупа"
                },
                new ProductionOrder
                {
                    Id = 57,
                    Number = 5,
                    ClientName = "Петя"
                },
                new ProductionOrder
                {
                    Id = 58,
                    Number = 6,
                    ClientName = "Женя"
                }
            };

            var result = new Mock<IOrdersRepository>();
            result.Setup(x => x.GetOrders()).Returns(orders);
            return result.Object;
        }
    }
}
