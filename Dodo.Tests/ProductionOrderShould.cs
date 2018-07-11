using System;
using Dodo.Tracker.Contracts;
using Xunit;

namespace Dodo.Tests
{
    public class ProductionOrderShould
    {
        [Fact]
        public void ReturnTrue_WhenIsExpiringIsCalled_AndNowParameterIsGreaterThanChangeDateAtLeastForOneHour()
        {
            var order = new ProductionOrder()
            {
                ChangeDate = DateTime.Parse("07/11/2018 23:00")
            };

            var isExpiring = order.IsExpiring(DateTime.Parse("07/11/2018 21:59"));
            
            Assert.True(isExpiring);
        }
        
        [Fact]
        public void ReturnFalse_WhenIsExpiringIsCalled_AndNowParameterIsLessThanChangeDate()
        {
            
        }
        
        [Fact]
        public void ReturnFalse_WhenIsExpiringIsCalled_AndNowParameterIsEqualToChangeDate()
        {
            
        }
    }
}