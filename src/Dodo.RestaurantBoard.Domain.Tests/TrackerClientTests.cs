using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using NUnit.Framework;

namespace Dodo.RestaurantBoard.Domain.Tests
{
    public class TrackerClientShould
    {
        [Test]
        public void ReturnActualNumberOfItems_WhenStationaryOrderTypeRequested()
        {
            // Act
            var actual = GetOrdersByType(OrderType.Stationary);

            // Assert
            Assert.AreEqual(4, actual.Length);
        }

        private static ProductionOrder[] GetOrdersByType(OrderType orderType)
        {
            var client = new TrackerClient();
            return client.GetOrdersByType(null, orderType, null, 0);
        }
    }
}