using Dodo.Core.DomainModel.Departments;
using Xunit;

namespace Dodo.Core.Tests
{
    public class CallCenterPhoneParameterShould
    {
        [Fact]
        public void ReturnTrueIfNumberIsEmpty()
        {
            var callCenterPhoneParameter = new CallCenterPhoneParameter();

            var number = "";

            Assert.True(callCenterPhoneParameter.CheckNumberIsNullOrEmpty(number));
        }

        [Fact]
        public void ReturnFalseIfNumberIsNotEmpty()
        {
            var callCenterPhoneParameter = new CallCenterPhoneParameter();

            var number = "number";

            Assert.False(callCenterPhoneParameter.CheckNumberIsNullOrEmpty(number));
        }
    }
}
