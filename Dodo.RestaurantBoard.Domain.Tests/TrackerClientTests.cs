using System;
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
        #region State tests
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
        #endregion State tests

        #region Behaviour tests
        [Test]
        public void GetOrdersByType_CallsRepositoryGetOrders_Once()
        {
            var mockRepository = new Mock<IOrdersRepository>();
            var trackerClient = new TrackerClient(mockRepository.Object);

            var orders = GetOrders(trackerClient);

            mockRepository.Verify(x => x.GetOrders(), Times.Once());
        }

        [Test]
        public void AddOrder_ThrowsException_WhenClientNameIsNotSpecified()
        {
            var dummyRepository = new Mock<IOrdersRepository>();
            var trackerClient = new TrackerClient(dummyRepository.Object);
            var productionOrder = new ProductionOrder()
            {
                ClientName = string.Empty
            };

            Assert.Throws<Exception>(() => trackerClient.AddOrder(productionOrder));
        }

        [Test]
        public void AddOrder_CallsRepositoryAddOrder_WhenClientNameIsSpecified()
        {
            var mockRepository = new Mock<IOrdersRepository>();
            var trackerClient = new TrackerClient(mockRepository.Object);
            var productionOrder = new ProductionOrder()
            {
                ClientName = "John Doe"
            };

            trackerClient.AddOrder(productionOrder);

            mockRepository.Verify(x => x.AddOrder(productionOrder), Times.Once);
        }

        [Test]
        public void DeleteOrder_ThrowsException_WhenOrderNotFound()
        {
            var mockRepository = new Mock<IOrdersRepository>();
            mockRepository.Setup(x => x.GetOrder(It.IsAny<int>())).Returns(() => null);

            var trackerClient = new TrackerClient(mockRepository.Object);

            Assert.Throws<Exception>(() => trackerClient.DeleteOrder(42));
        }

        [Test]
        public void DeleteOrder_CallsRepositoryDeleteOrder_WhenOrderFound()
        {
            var mockRepository = new Mock<IOrdersRepository>();
            mockRepository.Setup(x => x.GetOrder(It.IsAny<int>())).Returns(new ProductionOrder());
            var trackerClient = new TrackerClient(mockRepository.Object);

            trackerClient.DeleteOrder(42);

            mockRepository.Verify(x => x.DeleteOrder(It.IsAny<int>()), Times.Once);
        }
        #endregion

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
