using System;
using System.Xml.Linq;
using Xunit;
using Dodo.Core.DomainModel.Departments;

namespace Dodo.Core.UnitTests
{
    public class CallCenterPhoneShould
    {
        [Fact]
        public void KeepOnlyDigitsInPhone_WhenPhoneIsStringWithSpacesDashesAndBraces()
        {
            var phoneParameter = new CallCenterPhoneParameter
            {
                Number = "8 (922) 123-45-67"
            };

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;
            
            Assert.Equal("89221234567", numberWithoutMarks);
        }
        
        [Fact]
        public void ReturnNull_WhenPhoneHasNotBeenSet()
        {
            var phoneParameter = new CallCenterPhoneParameter();

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;
            
            Assert.Null(numberWithoutMarks);
        }
        
        [Fact]
        public void ReturnEmptyString_WhenPhoneIsEmptyString()
        {
            var phoneParameter = new CallCenterPhoneParameter
            {
                Number = string.Empty
            };

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;
            
            Assert.Equal("", numberWithoutMarks);
        }

        [Fact]
        public void CreateFullPath_ByConcatinateHostAndFileName()
        {
            var phoneParameter = new CallCenterPhoneParameter
            {
                IconPath = @"icons\2.png"
            };

            var host = "http://dodopizza.com"; 
            var url = phoneParameter.GetIconUrl(host);
                
            Assert.Equal("http://dodopizza.com/icons/2.png", url);
        }
        
        [Fact]
        public void CreateXmlNode_ByKnownParameters()
        {
            var phoneParameter = new CallCenterPhoneParameter
            {
                IconPath = @"icons\2.png",
                Number = "8 (902) 123-45-67"
            };

            var node = phoneParameter.CreateXmlNode();
            
            var expectedNode = new XElement("Phone",
                new XAttribute("number", "8 (902) 123-45-67"),
                new XAttribute("iconPath", @"icons\2.png"),
                new XAttribute("iconSitePath", ""));
            Assert.Equal(expectedNode.ToString(), node.ToString());
        }
    }
}