using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using System;
using Xunit;


namespace Dodo.Tests
{
    public class TrackerClientShould
    {                               
        [Fact]
        public void ReturnAllOrders_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var expectedOrders = new ProductionOrder[]
            {
                new ProductionOrder(),
                new ProductionOrder()
            };
            var ordersProviderStub = new Mock<IOrdersProvider>();
            var dateProviderDummy = new DateProvider();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            var trackerClient = new TrackerClient(ordersProviderStub.Object, dateProviderDummy);

            var actualOrders = GetOrdersWithoutExpiringOnlyParameter(trackerClient);
            
            Assert.Equal(expectedOrders, actualOrders);
        }

        [Fact]
        public void ReturnOnlyExpiringOrders_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var dateProviderStub = new Mock<IDateProvider>();
            dateProviderStub.Setup(p => p.Now()).Returns(new DateTime(2018, 07, 11, 23, 00, 00));
            var notExpiringOrder = new ProductionOrder
            {
                Id = 1,
                Number = 3,
                ClientName = "Misha",
                ChangeDate = new DateTime(2018, 07, 11, 22, 00, 00)
            };
            var expiringOrder = new ProductionOrder
            {
                Id = 2,
                Number = 4,
                ClientName = "Tanya",
                ChangeDate = new DateTime(2018, 07, 11, 21, 59, 00)
            };
            var expectedOrders = new ProductionOrder[]
            {
                notExpiringOrder,
                expiringOrder
            };
            var ordersProviderStub = new Mock<IOrdersProvider>();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            var trackerClient = new TrackerClient(ordersProviderStub.Object, dateProviderStub.Object);

            var actualOrders = GetOrdersWithExpiringOnlyParameterEqualToTrue(trackerClient);

            Assert.Equal(new[]{ expiringOrder }, actualOrders);
        }


        [Fact]
        public void CallGetOrdersOnOrdersProvider_WhenGetOrdersIsCalled()
        {
            var ordersProviderMock = new Mock<IOrdersProvider>();
            var dateProviderDummy = new DateProvider();
            var trackerClient = new TrackerClient(ordersProviderMock.Object, dateProviderDummy);

            var orders = GetOrdersWithoutExpiringOnlyParameter(trackerClient);

            ordersProviderMock.Verify(op => op.GetOrders(), Times.Once);
        }
        
        [Fact]
        public void NotCallNowOnDateProvider_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var ordersProviderStub = new OrdersProvider();
            var dateProviderMock = new Mock<IDateProvider>();
            var trackerClient = new TrackerClient(ordersProviderStub, dateProviderMock.Object);

            var orders = GetOrdersWithoutExpiringOnlyParameter(trackerClient);

            dateProviderMock.Verify(dp => dp.Now(), Times.Never);
        }
        
        [Fact]
        public void CallNowOnDateProvider_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var ordersProviderStub = new OrdersProvider();
            var dateProviderMock = new Mock<IDateProvider>();
            var trackerClient = new TrackerClient(ordersProviderStub, dateProviderMock.Object);

            var orders = GetOrdersWithExpiringOnlyParameterEqualToTrue(trackerClient);

            dateProviderMock.Verify(dp => dp.Now(), Times.Once);
        }
        
        [Fact]
        public void NotCallIsExpiringOnEachProductionOrder_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var productionOrderMock = new Mock<ProductionOrder>();
            var expectedOrders = new ProductionOrder[]
            {
                productionOrderMock.Object
            };
            var ordersProviderStub = new Mock<IOrdersProvider>();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            var dateProviderDummy = new DateProvider();
            var trackerClient = new TrackerClient(ordersProviderStub.Object, dateProviderDummy);
            
            var orders = GetOrdersWithoutExpiringOnlyParameter(trackerClient);

            productionOrderMock.Verify(pom => pom.IsExpiring(It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public void CallIsExpiringOnEachProductionOrder_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var productionOrderMock = new Mock<ProductionOrder>();
            var expectedOrders = new ProductionOrder[]
            {
                productionOrderMock.Object
            };
            var ordersProviderStub = new Mock<IOrdersProvider>();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            var dateProviderStub = new DateProvider();
            var trackerClient = new TrackerClient(ordersProviderStub.Object, dateProviderStub);

            var orders = GetOrdersWithExpiringOnlyParameterEqualToTrue(trackerClient);

            productionOrderMock.Verify(pom => pom.IsExpiring(It.IsAny<DateTime>()), Times.Once);
        }
        
        
        private ProductionOrder[] GetOrdersWithoutExpiringOnlyParameter(ITrackerClient trackerClient)
        {
            return trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0);
        }
        
        private ProductionOrder[] GetOrdersWithExpiringOnlyParameterEqualToTrue(ITrackerClient trackerClient)
        {
            return trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0, true);
        }
    }
}