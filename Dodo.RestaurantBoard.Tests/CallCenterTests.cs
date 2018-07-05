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

        private XElement CreatePhoneXmlNode(string phone="", string iconPath="", string iconSitePath = "")
        {
            return new XElement("Phone",
                new XAttribute("number", phone),
                new XAttribute("iconPath", iconPath),
                new XAttribute("iconSitePath", iconSitePath));
        }





        [Fact]
        public void GetCallCenterPhoneFromXml_CallCenterPhoneXElementWithPhone_CallCenterPhoneParameterPhoneEqualXmlPhone()
        {
            var callCenterPhonesXml =
                CreatePhonesXmlNode(
                        CreatePhoneXmlNode(phone:defaultPhone)
                );

            var phone = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(callCenterPhonesXml).Single();
            var phoneNumber = phone.Number;

            Assert.Equal(defaultPhone, phoneNumber);
        }

        [Fact]
        public void GetCallCenterPhoneFromXml_CallCenterPhoneXElementWithSitePath_CallCenterPhoneParameterIconPathEqualXmlIconPath()
        {
            var callCenterPhonesXml =
                CreatePhonesXmlNode(
                        CreatePhoneXmlNode(iconPath:defaultIconPath)
                );

            var phone = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(callCenterPhonesXml).Single();
            var iconPath = phone.IconPath;

            Assert.Equal(defaultIconPath, iconPath);
        }

        [Fact]
        public void GetCallCenterPhoneFromXml_CallCenterPhoneXElementWIthIconSitePath_CallCenterPhoneParameterIconSitePathEqualXmlIconSitePath()
        {
            var callCenterPhonesXml =
                CreatePhonesXmlNode(
                        CreatePhoneXmlNode(iconSitePath:defaultIconSitePath)
                );

            var phone = CallCenterPhoneParameter.GetCallCenterPhonesFromXml(callCenterPhonesXml).Single();
            var iconSitePath = phone.IconSitePath;

            Assert.Equal(defaultIconSitePath, iconSitePath);
        }

      

        [Fact]
        public void GetCallCenterPhonesFromXml_CallCenterPhonesXElement3Phones_Contains3Phones()
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
        public void GetPhoneNumberWithoutMarks_CallCenterPhoneParameterWithMarks_NotContainsMarks()
        {
            var ccPhoneParameter = new CallCenterPhoneParameter { Number = "123-456( 789)" };

            var phoneWithoutMarks = ccPhoneParameter.NumberWithoutMarks;

            Assert.Equal("123456789", phoneWithoutMarks);
        }

    }
}
