using System;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
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
            var stubRepository = CreateRepository(ordersCount: 2);
            var trackerClient = new TrackerClient(stubRepository);
            
            var orders = GetOrders(trackerClient, limit: 1);

            Assert.AreEqual(1, orders.Length);
        }

        [Test]
        public void GetOrdersByType_ReturnsOrdersUpToTheRepositoryCount_WhenLimitExceedsRepositoryCount()
        {
            var stubRepository = CreateRepository(ordersCount: 1);
            var trackerClient = new TrackerClient(stubRepository);

            var orders = GetOrders(trackerClient, limit: 2);

            Assert.AreEqual(1, orders.Length);
        }
        #endregion State tests

        #region Behaviour tests
        [Test]
        public void GetOrdersByType_CallsRepositoryGetOrders_Once()
        {
            var repositoryMock = new Mock<IOrdersRepository>();
            var trackerClient = new TrackerClient(repositoryMock.Object);

            var orders = GetOrders(trackerClient);

            repositoryMock.Verify(x => x.GetOrders(), Times.Once());
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

            Assert.Throws<ArgumentNullException>(() => trackerClient.AddOrder(productionOrder));
        }

        [Test]
        public void AddOrder_CallsRepositoryAddOrder_WhenClientNameIsSpecified()
        {
            var repositoryMock = new Mock<IOrdersRepository>();
            var trackerClient = new TrackerClient(repositoryMock.Object);
            var productionOrder = new ProductionOrder()
            {
                ClientName = "John Doe"
            };

            trackerClient.AddOrder(productionOrder);

            repositoryMock.Verify(x => x.AddOrder(productionOrder), Times.Once);
        }

        [Test]
        public void DeleteOrder_ThrowsException_WhenOrderNotFound()
        {
            var repositoryMock = new Mock<IOrdersRepository>();
            repositoryMock.Setup(x => x.GetOrder(It.IsAny<int>())).Returns(() => null);

            var trackerClient = new TrackerClient(repositoryMock.Object);

            Assert.Throws<Exception>(() => trackerClient.DeleteOrder(42));
        }

        [Test]
        public void DeleteOrder_CallsRepositoryDeleteOrder_WhenOrderFound()
        {
            var repositoryMock = new Mock<IOrdersRepository>();
            repositoryMock.Setup(x => x.GetOrder(It.IsAny<int>())).Returns(new ProductionOrder());
            var trackerClient = new TrackerClient(repositoryMock.Object);

            trackerClient.DeleteOrder(42);

            repositoryMock.Verify(x => x.DeleteOrder(42), Times.Once);
        }
        #endregion

        private static ProductionOrder[] GetOrders(TrackerClient trackerClient, int limit = 42)
        {
            return trackerClient.GetOrdersByType(Uuid.Empty, default(OrderType), new OrderState[0], limit);
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
