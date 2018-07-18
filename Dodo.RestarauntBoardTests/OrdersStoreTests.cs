using System;
using System.Collections.Generic;
using Dodo.RestaurantBoard.Domain.Stores;
using Dodo.Tracker.Contracts;
using Moq;
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
            var order = Create.OrderToWatch;
            var orderStore = Create.OrderStore.With(order).Please();

            orderStore.GetExpiredOrders("01-01-2018");

            order.VerifyThatIsExpiredWasCalledOnce();
        }

        // Behaviour
        [Fact]
        public void IsExpiredShoudNotInvokeOnAnyOrder_WhenGetOrders()
        {
            var order = Create.OrderToWatch;
            var orderStore = Create.OrderStore.With(order).Please();

            orderStore.GetOrders();

            order.VerifyIsExpiredWasNeverCalled();
        }

        private static void AssertThat(bool expression)
        {
            Assert.True(expression);
        }

    }

    internal static class Create
    {
        public static OrdersStoreBuilder OrderStore => new OrdersStoreBuilder();
        public static ProductionOrderMockBuilder OrderToWatch => new ProductionOrderMockBuilder();
        public static ProductionOrderBuilder Order => new ProductionOrderBuilder();
    }

    public class ProductionOrderMockBuilder
    {
        private Mock<IProductionOrder> _orderMock;

        public ProductionOrderMockBuilder()
        {
            _orderMock = new Mock<IProductionOrder>();
        }

        internal ProductionOrderMockBuilder WithDate(string dateString)
        {
            var date = DateTime.Parse(dateString);

            return this;
        }

        internal IProductionOrder GetMock()
        {
            return _orderMock.Object;
        }


        internal void VerifyThatIsExpiredWasCalledOnce()
        {
            _orderMock.Verify(p => p.IsExpired(It.IsAny<DateTime>()), Times.Once);
        }

        internal void VerifyIsExpiredWasNeverCalled()
        {
            _orderMock.Verify(p => p.IsExpired(It.IsAny<DateTime>()), Times.Never);
        }
    }

    public class ProductionOrderBuilder
    {
        private ProductionOrder _order;

        public ProductionOrderBuilder()
        {
            _order = new ProductionOrder();
        }

        internal ProductionOrderBuilder WithDate(string dateString)
        {
            var date = DateTime.Parse(dateString);

            return this;
        }

        internal ProductionOrder Please()
        {
            return _order;
        }
    }

    public class OrdersStoreBuilder
    {
        private OrdersStore _ordersStore;

        public OrdersStoreBuilder()
        {
            _ordersStore = new OrdersStore();
        }

        internal OrdersStore Please()
        {
            return _ordersStore;
        }

        internal OrdersStoreBuilder With(IProductionOrder order)
        {
            _ordersStore.AddOrder(order);
            return this;
        }

        internal OrdersStoreBuilder With(ProductionOrderMockBuilder orderMockBuilder)
        {
            _ordersStore.AddOrder(orderMockBuilder.GetMock());
            return this;
        }
    }

    public static class OrderStoreExtensions
    {
        public static void GetExpiredOrders(this OrdersStore orderStore, string dateString)
        {
            var date = DateTime.Parse(dateString);
            orderStore.GetExpiredOrders(date);
        }
    }

    public static class ProductioOrderExtensions
    {
        public static DateTime ExpireDate(this IProductionOrder order)
        {
            return order.OrderDate.AddSeconds((new ProductionOrder()).ExpirationTime + 1);
        }

        public static DateTime Date(this IProductionOrder order)
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