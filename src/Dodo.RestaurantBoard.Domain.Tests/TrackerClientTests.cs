using System;
using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts.Enums;
using Xunit;

namespace Dodo.RestaurantBoard.Domain.Tests
{
    public class TrackerClientTests
    {
        [Fact]
        public void GetOrdersByLimit_ShouldReturnAmountOfOrders_LessThanLimit()
        {
            var trackerClient = new TrackerClient();

            var orders = trackerClient.GetOrdersByLimit(new Uuid(), OrderType.Delivery, new OrderState[] { }, limit: 1);
            
            Assert.Equal(1, orders.Length);
        }
    }
}