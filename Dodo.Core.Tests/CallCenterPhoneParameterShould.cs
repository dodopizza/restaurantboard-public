using System.Linq;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.Tests.DSL;
using Moq;
using Xunit;

namespace Dodo.Core.Tests
{
    public class CallCenterPhoneParameterShould
    {
        [Fact]
        public void ReturnCallCenterPhoneParameter_WithNumberValue1_AndIconPathValue2_AndIconSitePathValue3()
        {
            var callCenterPhonesXElement = Create.XElement("CallCenterPhones").Please();
            var phoneXElement = Create.XElement("Phone")
                .WithNumber("1")
                .WithIconPath("2")
                .WithIconSitePath("3")
                .Please();
            callCenterPhonesXElement.Add(phoneXElement);
            var callCenterPhoneParameter = new CallCenterPhoneParameter();

            var callCenterPhoneParameters = callCenterPhoneParameter
                .GetCallCenterPhonesParameters(callCenterPhonesXElement)
                .ToArray();
            
            Assert.Equal("1", callCenterPhoneParameters[0].Number);
            Assert.Equal("2", callCenterPhoneParameters[0].IconPath);
            Assert.Equal("3", callCenterPhoneParameters[0].IconSitePath);
        }
    }
}