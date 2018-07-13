using System;
using System.Collections.Generic;
using System.Text;
using Dodo.Tracker.Contracts;
using Xunit;

namespace Dodo.RestarauntBoardTests
{
    public class ProducOrderTests
    {
        [Fact]
        public void ShoudBeExpired_WhenPlace11SecondsAgo()
        {
            var orderDate = new DateTime(2018, 1, 1, 12, 12, 12);
            var order = new ProductionOrder()
            {
                OrderDate = orderDate
            };
            
            Assert.True(order.IsExpired(orderDate.AddSeconds(11)));

        }
    }
}
