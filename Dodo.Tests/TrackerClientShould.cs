using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using Xunit;


namespace Dodo.Tests
{
    public class TrackerClientShould
    {
        [Fact]
        public void ReturnAllOrders_IfIsExpiringParameterNotSpecifiedAndDefaultValueIsUsed()
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
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            var trackerClient = new TrackerClient(ordersProviderStub.Object);

            var actualOrders = trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0);
            
            Assert.Equal(expectedOrders, actualOrders);
        }

        [Fact]
        public void ReturnOnlyExpiringOrders_IfIsExpiringParameterIsEqualToTrue()
        {
            
        }
    }
}