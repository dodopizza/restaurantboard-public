using System;
using Dodo.Tracker.Contracts;
using Xunit;

namespace Dodo.Tests
{
    public class ProductionOrderShould
    {
        [Fact]
        public void NotExpire_If60MinutesHasElapsed()
        {
            var order = new ProductionOrder()
            {
                ChangeDate = new DateTime(2018, 07, 11, 21, 00, 00)
            };

            var isExpiring = order.IsExpiring(new DateTime(2018, 07, 11, 22, 00, 00));

            Assert.False(isExpiring);
        }

        [Fact]
        public void Expire_If61MinutesHasElapsed()
        {
            var order = new ProductionOrder()
            {
                ChangeDate = new DateTime(2018, 07, 11, 21, 00, 00)
            };

            var isExpiring = order.IsExpiring(new DateTime(2018, 07, 11, 22, 01, 00));

            Assert.True(isExpiring);
        }
        
        [Fact]
        public void NotExpire_IfDateOfCreationIsUnknown()
        {
            var order = new ProductionOrder();

            var isExpiring = order.IsExpiring(new DateTime(2018, 07, 11, 22, 01, 00));

            Assert.False(isExpiring);
        }
    }
}