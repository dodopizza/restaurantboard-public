using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Dodo.Tracker.Contracts;
using Xunit;

namespace Dodo.Tests
{
    public class TrackerClientShould
    {
        [Fact]
        public void ReturnSingleProductOrder_WhenLimitOne()
        {
            var trackerClient = new TestableTrackerClient();

            var orders = trackerClient.GetOrdersByCount(new Uuid(), Tracker.Contracts.Enums.OrderType.Delivery,null,1);

            Assert.Single(orders);
        }


        public class TestableTrackerClient:TrackerClient
        {
            public override ProductionOrder[] QueryOrders()
            {
                var orders = new[]
                {
                    new ProductionOrder
                    {
                        Id = 55,
                        Number = 3,
                        ClientName = "Пупа"
                    },
                    new ProductionOrder
                    {
                        Id = 56,
                        Number = 4,
                        ClientName = "Лупа"
                    },
                };

                return orders;
            }
        }
    }
}
