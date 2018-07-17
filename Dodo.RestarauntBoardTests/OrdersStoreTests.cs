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
            var productOrder = new ProductionOrder();
            var orderStore = new OrdersStore();

            orderStore.AddOrder(productOrder);

            Assert.Contains(productOrder, orderStore.GetOrders());
        }

        //State
        [Fact]
        public void ShoudContainExpiredOrder_WhenGetExpiredOrders()
        {
            var orderDate = new DateTime(2018, 1, 1);
            var order = new ProductionOrder(){OrderDate =  orderDate };
            var orderStore = new OrdersStore();
            orderStore.AddOrder(order);

            var expiredOrders = orderStore.GetExpiredOrders(orderDate.AddSeconds(order.ExpirationTime+1));

            Assert.Contains(order, expiredOrders);
        }

        //State
        [Fact]
        public void ShoudNotContainUnExpiredOrder_WhenGetExpiredOrders()
        {
           
            var orderDate = new DateTime(2018, 1, 1);
            var order = new ProductionOrder(){OrderDate =  orderDate };
            var orderStore = new OrdersStore();
            orderStore.AddOrder(order);

            var expiredOrders = orderStore.GetExpiredOrders(orderDate);

            Assert.Empty(expiredOrders);
        }


      
        //State
        [Fact]
        public void ShoudContainAllOrders_WhenGetOrders()
        {
            var order1 = new ProductionOrder();
            var order2 = new ProductionOrder();
            var orderStore = new OrdersStore();
            orderStore.AddOrder(order1);
            orderStore.AddOrder(order2);

            var allOrders = orderStore.GetOrders();

            Assert.Contains(order1, allOrders);
            Assert.Contains(order2, allOrders);
        }

        // Behaviour
        [Fact]
        public void IsExpiredShoudInvokeOncePerOrder_WhenGetExpiredOrders()
        {
            var productOrderMock = new Mock<IProductionOrder>();
            var order = productOrderMock.Object;
            var orderStore = new OrdersStore();
            orderStore.AddOrder(order);

            orderStore.GetExpiredOrders(new DateTime(2018, 1, 1));

            productOrderMock.Verify(p=>p.IsExpired(It.IsAny<DateTime>()),Times.Once);
        }

        // Behaviour
        [Fact]
        public void IsExpiredShoudNotInvokeOnAnyOrder_WhenGetOrders()
        {
            var productOrderMock = new Mock<IProductionOrder>();
            var order = productOrderMock.Object;
            var orderStore = new OrdersStore();
            orderStore.AddOrder(order);

            orderStore.GetOrders();

            productOrderMock.Verify(p => p.IsExpired(It.IsAny<DateTime>()), Times.Never);
        }
    }
}
