using Dodo.Core.DomainModel.Departments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class CallCenterTests
    {
        private const string defaultPhone = "123";
        private const string defaultIconPath = "some path";
        private const string defaultIconSitePath = "some icon site path";

        private XElement CreatePhonesXmlNode(params XElement[] childs)
        {
            return new XElement("root",
                    new XElement("CallCenterPhones",
                        childs
                ));
        }

        private XElement CreatePhoneXmlNode()
        {
            return new XElement("Phone",
                new XAttribute("number", defaultPhone),
                new XAttribute("iconPath", defaultIconPath),
                new XAttribute("iconSitePath", defaultIconSitePath));
        }





        [Fact]
        public void ShouldHaveCorrectValues()
        {
            var callCenterPhonesXml =
                CreatePhonesXmlNode(
                        CreatePhoneXmlNode()
                );
            var phone = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(callCenterPhonesXml).Single();
            Assert.Equal(defaultPhone, phone.Number);
            Assert.Equal(defaultIconPath, phone.IconPath);
            Assert.Equal(defaultIconSitePath, phone.IconSitePath);

        }

        [Fact]
        public void ShouldNotContainsMarks()
        {
            var ccPhoneParameter = new CallCenterPhoneParameter { Number = "123-456( 789)" };
            Assert.Equal("123456789", ccPhoneParameter.NumberWithoutMarks);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(10000)]
        [InlineData(1000000)]
        public void ShoudContainsCorrectPhonesCount(int initCount)
        {
            var callCenterPhonesXml =
               CreatePhonesXmlNode(
                    Enumerable.Range(0, initCount).Select(i => CreatePhoneXmlNode()).ToArray()
               );
            var output = callCenterPhonesXml.ToString();

            var phones = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(callCenterPhonesXml);
            Assert.Equal(initCount, phones.Length);

        }
    }
}
