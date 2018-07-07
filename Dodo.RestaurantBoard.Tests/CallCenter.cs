using Dodo.Core.DomainModel.Departments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class CallCenter
    {
        private XElement CreatePhonesXmlNode(params XElement[] childs)
        {
            return new XElement("root",
                    new XElement("CallCenterPhones",
                        childs
                ));
        }

        private XElement CreatePhoneXmlNode(string phone="", string iconPath="", string iconSitePath = "")
        {
            return new XElement("Phone",
                new XAttribute("number", phone),
                new XAttribute("iconPath", iconPath),
                new XAttribute("iconSitePath", iconSitePath));
        }

        [Fact]
        public void ShouldSetPhone_FromXmlElement()
        {
            var callCenterPhonesXml =
                CreatePhonesXmlNode(
                        CreatePhoneXmlNode(phone:"+79261234567")
                );

            var phone = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(callCenterPhonesXml).Single();
            var phoneNumber = phone.Number;

            Assert.Equal("+79261234567", phoneNumber);
        }

        [Fact]
        public void ShouldSetIconPath_FromXmlElement()
        {
            var callCenterPhonesXml =
                CreatePhonesXmlNode(
                        CreatePhoneXmlNode(iconPath: @"C:\temp\restaurantboard-public\icon.ico")
                );

            var phone = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(callCenterPhonesXml).Single();
            var iconPath = phone.IconPath;

            Assert.Equal(@"C:\temp\restaurantboard-public\icon.ico", iconPath);
        }

        [Fact]
        public void ShouldSetIconSitePath_FromXmlElement()
        {
            var callCenterPhonesXml =
                CreatePhonesXmlNode(
                        CreatePhoneXmlNode(iconSitePath: "dodopizza/restaurantboard-public/icon.ico")
                );

            var phone = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(callCenterPhonesXml).Single();
            var iconSitePath = phone.IconSitePath;

            Assert.Equal("dodopizza/restaurantboard-public/icon.ico", iconSitePath);
        }

      

        [Fact]
        public void ShouldReadAllPhones_FromXmlElement()
        {
            var callCenterPhonesXml =
               CreatePhonesXmlNode(
                    CreatePhoneXmlNode(),
                    CreatePhoneXmlNode(),
                    CreatePhoneXmlNode()
               );
            
            var phones = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(callCenterPhonesXml);

            Assert.Equal(3, phones.Length);
        }

        [Fact]
        public void NumberWithoutMarksContainOnlyDigits_WhenNumberContainBrackets()
        {
            var ccPhoneParameter = new CallCenterPhoneParameter { Number = "8(926)1234567" };

            var phoneWithoutMarks = ccPhoneParameter.NumberWithoutMarks;

            Assert.Equal("89261234567", phoneWithoutMarks);
        }

    }
}
