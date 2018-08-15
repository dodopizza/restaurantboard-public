using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.Tests
{
    public class TrackerClientShould
    {
        [Fact]
        public void ReturnSingleProductOrder_WhenLimitOne()
        {
            var trackerClient = new TrackerClient();

            var orders = trackerClient.GetOrdersByCount(new Uuid(), Tracker.Contracts.Enums.OrderType.Delivery,null,1);

            Assert.Single(orders);
        }
    }
}
