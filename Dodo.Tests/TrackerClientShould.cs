using Dodo.Tests.DSL;
using Xunit;

namespace Dodo.Tests
{
    public class TrackerClientShould
    {                               
        [Fact]
        public void ReturnAllOrders_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var expectedOrders = Create.Orders(2);
            var ordersProvider = Create.OrdersProvider.AddOrders(expectedOrders).Please();
            var trackerClient = Create.TrackerClient.WithOrdersProviderAs(ordersProvider).Please();
            
            var actualOrders = trackerClient.GetOrdersWithoutExpiringOnlyParameter();
            
            Assert.Equal(expectedOrders, actualOrders);
        }

        [Fact]
        public void ReturnOnlyExpiringOrders_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var dateProvider = Create.DateProvider.WithNowAs(11.July(2018).TimeIs("23:00")).Please();
            var notExpiringOrder = Create.Order.WithChangeDate(11.July(2018).TimeIs("22:00")).Please();
            var expiringOrder = Create.Order.WithChangeDate(11.July(2018).TimeIs("21:59")).Please();
            var ordersProvider = Create.OrdersProvider.AddOrder(notExpiringOrder).AddOrder(expiringOrder).Please();
            var trackerClient = Create.TrackerClient.WithDateProviderAs(dateProvider).WithOrdersProviderAs(ordersProvider).Please();

            var actualOrders = trackerClient.GetOrdersWithExpiringOnlyParameterEqualToTrue();

            Assert.Equal(new[]{ expiringOrder }, actualOrders);
        }

        [Fact]
        public void CallGetOrdersOnOrdersProvider_WhenGetOrdersIsCalled()
        {
            var ordersProvider = Create.OrdersProvider;
            var trackerClient = Create.TrackerClient.WithOrdersProviderAs(ordersProvider).Please();

            trackerClient.GetOrdersWithoutExpiringOnlyParameter();

            VerifyThat.GetOrdersCallOnceIn(ordersProvider);
        }
        
        [Fact]
        public void NotCallNowOnDateProvider_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var dateProvider = Create.DateProvider.Please();
            var trackerClient = Create.TrackerClient.WithDateProviderAs(dateProvider).Please();

            trackerClient.GetOrdersWithoutExpiringOnlyParameter();
            
            VerifyThat.NowNeverCalledIn(dateProvider);
        }
        
        [Fact]
        public void CallNowOnDateProvider_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var dateProvider = Create.DateProvider;
            var trackerClient = Create.TrackerClient.WithDateProviderAs(dateProvider).Please();            

            trackerClient.GetOrdersWithExpiringOnlyParameterEqualToTrue();

            VerifyThat.NowCalledOnceIn(dateProvider);
        }       
    }
}