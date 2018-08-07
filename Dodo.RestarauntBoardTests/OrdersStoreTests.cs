using Dodo.RestarauntBoardTests.DslTools;
using Dodo.RestaurantBoard.Domain.Stores;
using Dodo.Tracker.Contracts;
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
            var order = Create.Order.WithDate(1.January(2018)).Please();
            var orderStore = Create.OrderStore.With(order).Please();

            var expiredOrders = orderStore.GetExpiredOrders(order.ExpireDate());

            AssertThat(expiredOrders.Contains(order));
        }

        //State
        [Fact]
        public void ShoudNotContainUnExpiredOrder_WhenGetExpiredOrders()
        {
            var order = Create.Order.WithDate(1.January(2018)).Please();
            var orderStore = Create.OrderStore.With(order).Please();

            var expiredOrders = orderStore.GetExpiredOrders(order.Date());

            Assert.Empty(expiredOrders);
        }

        //State
        [Fact]
        public void ShoudContainAllOrders_WhenGetOrders()
        {
            var orderWithPizza = Create.Order.Please();
            var orderWithCola = Create.Order.Please();
            var orderStore = Create.OrderStore.With(orderWithPizza).With(orderWithCola).Please();

            var orderFromStore = orderStore.GetOrders();

            AssertThat(orderFromStore.Contains(orderWithPizza, orderWithCola));
        }

        // Behaviour
        [Fact]
        public void IsExpiredShoudInvokeOncePerOrder_WhenGetExpiredOrders()
        {
            var order = Create.OrderToWatch;
            var orderStore = Create.OrderStore.With(order).Please();

            orderStore.GetExpiredOrders("01-01-2018");

            order.VerifyThat.IsExpiredWasCalledOnce();
        }

        // Behaviour
        [Fact]
        public void IsExpiredShoudNotInvokeOnAnyOrder_WhenGetOrders()
        {
            var order = Create.OrderToWatch;
            var orderStore = Create.OrderStore.With(order).Please();

            orderStore.GetOrders();

            order.VerifyThat.IsExpiredWasNeverCalled();
        }

        private static void AssertThat(bool expression)
        {
            Assert.True(expression);
        }
    }
}