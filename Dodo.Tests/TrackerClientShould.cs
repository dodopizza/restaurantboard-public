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
                new ProductionOrder
                {
                    Id = 1,
                    Number = 3,
                    ClientName = "Misha"
                },
                new ProductionOrder
                {
                    Id = 2,
                    Number = 4,
                    ClientName = "Tanya"
                },
            };
            var ordersProviderStub = new Mock<IOrdersProvider>();
            var dateProviderStub = new DateProvider();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            var trackerClient = new TrackerClient(ordersProviderStub.Object, dateProviderStub);

            var actualOrders = trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0);
            
            Assert.Equal(expectedOrders, actualOrders);
        }

        [Fact]
        public void ReturnOnlyExpiringOrders_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var fakeDateProvider = new Mock<IDateProvider>();
            fakeDateProvider.Setup(p => p.Now()).Returns(DateTime.Parse("07/11/2018 23:00"));
            var expectedOrders = new ProductionOrder[]
            {
                new ProductionOrder
                {
                    Id = 1,
                    Number = 3,
                    ClientName = "Misha",
                    ChangeDate = DateTime.Parse("07/11/2018 22:00")
                },
                new ProductionOrder
                {
                    Id = 2,
                    Number = 4,
                    ClientName = "Tanya",
                    ChangeDate = DateTime.Parse("07/11/2018 22:01")
                },
            };
            var ordersProviderStub = new Mock<IOrdersProvider>();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            var trackerClient = new TrackerClient(ordersProviderStub.Object, fakeDateProvider.Object);

            var actualOrders = trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0, true);

            foreach(var order in actualOrders)
            {
                Assert.True(order.IsExpiring(DateTime.Parse("07/11/2018 23:00")));
            }
        }





        [Fact]
        public void CallGetOrdersOnOrdersProvider_WhenGetOrdersIsCalled()
        {
            
        }
        
        [Fact]
        public void NotCallNowOnDateProvider_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            
        }
        
        [Fact]
        public void CallNowOnDateProvider_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            
        }
    }
}