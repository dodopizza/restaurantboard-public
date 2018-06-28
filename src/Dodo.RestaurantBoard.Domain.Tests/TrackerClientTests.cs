using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts.Enums;
using NUnit.Framework;

namespace Dodo.RestaurantBoard.Domain.Tests
{
    public class TrackerClientTests
    {
        [Test]
        public void GetOrdersByType_ReturnsActualNumberOfItems()
        {
            // Init
            var client = new TrackerClient();

            // Act
            var actual = client.GetOrdersByType(null, default(OrderType), null, 0);

            // Assert
            Assert.AreEqual(4, actual.Length);
        }
    }
}