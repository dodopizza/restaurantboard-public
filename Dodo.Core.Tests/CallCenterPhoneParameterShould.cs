using System;
using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Xunit;

namespace Dodo.Core.Tests
{    
    public class CallCenterPhoneParameterShould
    {        
        [Fact]
        public void ReturnNumberWithoutMarksWithoutChanges_IfNumberIsNull()
        {
            const string number = null;
            var callCenterPhoneParameter = new CallCenterPhoneParameter();
            callCenterPhoneParameter.Number = number;
            
            var numberWithoutMarks = callCenterPhoneParameter.NumberWithoutMarks;
            
            Assert.Null(numberWithoutMarks);
        }
        
        [Fact]
        public void ReturnNumberWithoutMarksWithoutChanges_IfNumberIsEmpty()
        {
            const string number = "";
            var callCenterPhoneParameter = new CallCenterPhoneParameter();
            callCenterPhoneParameter.Number = number;
            
            var numberWithoutMarks = callCenterPhoneParameter.NumberWithoutMarks;
            
            Assert.Equal("", numberWithoutMarks);
        }

        [Fact]
        public void ReturnNumberWithoutMarksWithoutSplitters_IfNumberContainsSplitters()
        {
            const string number = "(867) 53-09";
            var callCenterPhoneParameter = new CallCenterPhoneParameter();
            callCenterPhoneParameter.Number = number;

            var numberWithoutMarks = callCenterPhoneParameter.NumberWithoutMarks;
            
            Assert.Equal("8675309", numberWithoutMarks);
        }
    }
}