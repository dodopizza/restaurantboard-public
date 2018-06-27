using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Xunit;

namespace Dodo.Core.Tests
{
    public class CallCenterPhoneParameterTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NumberWithoutMarks__IfNumberIsNullOrEmpty_ThenReturnsNumberWithoutChanges(string value)
        {
            var callCenterPhoneParameter = new CallCenterPhoneParameter
            {
                Number = value,
            };

            var numberWithoutMarks = callCenterPhoneParameter.NumberWithoutMarks;
            
            Assert.Equal(numberWithoutMarks, value);
        }
        
        [Theory]
        [InlineData("321-67", "32167")]
        [InlineData("(867) 53-09", "8675309")]
        public void NumberWithoutMarks__IfNumberWithSplitters_ThenReturnsNumberWithoutSplitters(string value)
        {
            var number
            var callCenterPhoneParameter = new CallCenterPhoneParameter
            {
                Number = value,
            };

            var numberWithoutMarks = callCenterPhoneParameter.NumberWithoutMarks;
            
            Assert.Equal(numberWithoutMarks, value);
        }
    }
}