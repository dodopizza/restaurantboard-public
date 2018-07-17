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
            var order = Create.Order.WithDate("01-01-2018").Please();
            var orderStore = Create.OrderStore.With(order).Please();

            var expiredOrders = orderStore.GetExpiredOrders(order.ExpireDate());

            Assert.Contains(order, expiredOrders);
        }

        //State
        [Fact]
        public void ShoudNotContainUnExpiredOrder_WhenGetExpiredOrders()
        {
            var order = Create.Order.WithDate("01-01-2018").Please();
            var orderStore = Create.OrderStore.With(order).Please();

            var expiredOrders = orderStore.GetExpiredOrders(order.Date());

            Assert.Empty(expiredOrders);
        }


      
        //State
        [Fact]
        public void ShoudContainAllOrders_WhenGetOrders()
        {
            var pizza = Create.Order.Please();
            var cola = Create.Order.Please();
            var orderStore = Create.OrderStore.With(pizza).With(cola).Please();

            var allOrders = orderStore.GetOrders();

            AssertThat(allOrders.Contains(pizza) && allOrders.Contains(cola));
        }

       

        // Behaviour
        [Fact]
        public void IsExpiredShoudInvokeOncePerOrder_WhenGetExpiredOrders()
        {
            var order = Create.Order;
            var orderStore = Create.OrderStore.With(order).Please();

            orderStore.GetExpiredOrders(new DateTime(2018, 1, 1));

            order.Verify(p=>p.IsExpired(It.IsAny<DateTime>()),Times.Once);
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

        private static void AssertThat(bool expression)
        {
            Assert.True(expression);
        }

    }

    internal static class Create
    {
        public static OrdersStoreBuilder OrderStore => new OrdersStoreBuilder();
        public static ProductionOrderBuilder Order => new ProductionOrderBuilder();

    }

    public class ProductionOrderBuilder
    {
        private Mock<ProductionOrder> _orderMock;

        public ProductionOrderBuilder()
        {
            _orderMock = new Mock<ProductionOrder>();
        }

        internal ProductionOrderBuilder WithDate(string dateString)
        {
            var date = DateTime.Parse(dateString);
            _orderMock.Object.OrderDate = date;

            return this;
        }

        internal ProductionOrder Please()
        {
            return _orderMock.Object;
        }

        internal Mock<ProductionOrder> Please()
        {
            return _orderMock;
        }
    }

    public class OrdersStoreBuilder
    {
        private OrdersStore _ordersStore;
        private Mock<ProductionOrder> _orderMock;

        public OrdersStoreBuilder()
        {
            _ordersStore = new OrdersStore();
            _orderMock = new Mock<ProductionOrder>();
        }

        internal OrdersStore Please()
        {
            return _ordersStore;
        }

        internal OrdersStoreBuilder With(ProductionOrderBuilder orderBuilder)
        {
            _orderMock = orderBuilder;
            return this;
        }

        public Verify(Action action, Moq.Times times)
        {

        }
    }

    public static class ProductioOrderExtensions
    {
        public static DateTime ExpireDate(this ProductionOrder order)
        {
            return order.OrderDate.AddMilliseconds(order.ExpirationTime + 1);
        }

        public static DateTime Date(this ProductionOrder order)
        {
            return order.OrderDate;
        }
    }

    public static class ListOfOrderExtension
    {
        public static List<IProductionOrder> Contain(this List<IProductionOrder> list, ProductionOrder order)
        {
            Assert.Contains(order, list);
            return list;
        }

        public static List<IProductionOrder> And(this List<IProductionOrder> list, ProductionOrder order)
        {
            return Contain(list, order);
        }
    }

}
