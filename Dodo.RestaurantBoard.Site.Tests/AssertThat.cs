using Dodo.Tracker.Contracts;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public static class AssertThat
    {
        public static void ReturnedOrdersContainsInfoFromExpected(IEnumerable<ProductionOrder> expected, IEnumerable<ReturnedOrder> actual)
        {
            var assertions = new List<Action<ReturnedOrder>>();

            foreach(var order in expected)
            {
                assertions.Add((item) =>
                {
                    Assert.Equal(order.ClientName, item.ClientName);
                    Assert.Equal(order.Id, item.OrderId);
                    Assert.Equal(order.Number, item.OrderNumber);
                });
            }

            Assert.Collection(actual, assertions.ToArray());
        }
    }
}
