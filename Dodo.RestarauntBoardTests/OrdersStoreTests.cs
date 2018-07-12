using System;
using System.Collections.Generic;
using System.Text;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Domain.Stores;
using Dodo.Tracker.Contracts;
using Moq;
using NLog.Filters;
using Xunit;

namespace Dodo.RestarauntBoardTests
{
    public class OrdersStoreTests
    {
        //State
        [Fact]
        public void ShouldContainSameOrder_WhenAddOrder()
        {
            var productOrderStub = new Mock<IProductionOrder>();
            var productOrder = productOrderStub.Object;
            var orderStore = new OrdersStore();


            orderStore.AddOrder(productOrder);

            Assert.Contains(productOrder, orderStore.GetOrders());
        }

        //State
        [Fact]
        public void ShoudContainExpiredOrder_WhenGetExpiredOrders()
        {
            var productOrderMock = new Mock<IProductionOrder>();
            productOrderMock.Setup(p => p.IsExpired(It.IsAny<DateTime>())).Returns(true);
            var expiredOrder = productOrderMock.Object;
            var orderStore = new OrdersStore();
            orderStore.AddOrder(expiredOrder);

            var expiredOrders = orderStore.GetExpiredOrders(new DateTime(2018, 1, 1));

            Assert.Contains(expiredOrder, expiredOrders);
        }

        //State
        [Fact]
        public void ShoudNotContainUnExpiredOrder_WhenGetExpiredOrders()
        {
            var productOrderMock = new Mock<IProductionOrder>();
            productOrderMock.Setup(p => p.IsExpired(It.IsAny<DateTime>())).Returns(false);
            var expiredOrder = productOrderMock.Object;
            var orderStore = new OrdersStore();
            orderStore.AddOrder(expiredOrder);

            var expiredOrders = orderStore.GetExpiredOrders(new DateTime(2018, 1, 1));

            Assert.Empty(expiredOrders);
        }


      
        //State
        [Fact]
        public void ShoudContainAllOrders_WhenGetOrders()
        {
            var productOrderMock1 = new Mock<IProductionOrder>();
            productOrderMock1.Setup(p => p.IsExpired(It.IsAny<DateTime>())).Returns(true);
            var expiredOrder = productOrderMock1.Object;
            var productOrderMock2 = new Mock<IProductionOrder>();
            productOrderMock2.Setup(p => p.IsExpired(It.IsAny<DateTime>())).Returns(false);
            var unExpiredOrder = productOrderMock2.Object;
            var orderStore = new OrdersStore();
            orderStore.AddOrder(expiredOrder);
            orderStore.AddOrder(unExpiredOrder);

            var allOrders = orderStore.GetOrders();

            Assert.Contains(expiredOrder, allOrders);
            Assert.Contains(unExpiredOrder, allOrders);
        }

        // Behaviour
        [Fact]
        public void IsExpiredShoudInvokeOnceOnEachOrders_WhenGetExpiredOrders()
        {
            var productOrderMock1 = new Mock<IProductionOrder>();
            var productOrderMock2 = new Mock<IProductionOrder>();
            var expiredOrder1 = productOrderMock1.Object;
            var expiredOrder2 = productOrderMock2.Object;

            var orderStore = new OrdersStore();
            orderStore.AddOrder(expiredOrder1);
            orderStore.AddOrder(expiredOrder2);

            orderStore.GetExpiredOrders(new DateTime(2018, 1, 1));

            productOrderMock1.Verify(p=>p.IsExpired(It.IsAny<DateTime>()),Times.Once);
            productOrderMock2.Verify(p=>p.IsExpired(It.IsAny<DateTime>()),Times.Once);
        }

        // Behaviour
        [Fact]
        public void IsExpiredShoudNotInvokeOnAnyOrder_WhenGetOrders()
        {
            var productOrderMock1 = new Mock<IProductionOrder>();
            var productOrderMock2 = new Mock<IProductionOrder>();
            var expiredOrder1 = productOrderMock1.Object;
            var expiredOrder2 = productOrderMock2.Object;

            var orderStore = new OrdersStore();
            orderStore.AddOrder(expiredOrder1);
            orderStore.AddOrder(expiredOrder2);

            orderStore.GetOrders();

            productOrderMock1.Verify(p => p.IsExpired(It.IsAny<DateTime>()), Times.Never);
            productOrderMock2.Verify(p => p.IsExpired(It.IsAny<DateTime>()), Times.Never);
        }


    }
}
