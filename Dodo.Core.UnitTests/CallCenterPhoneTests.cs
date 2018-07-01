﻿using System;
using System.Xml.Linq;
using Xunit;
using Dodo.Core.DomainModel.Departments;

namespace Dodo.Core.UnitTests
{
    public class CallCenterPhoneTests
    {
        [Fact]
        public void NumberWithoutMarksTest()
        {
            var callCenterPhoneParameter = new CallCenterPhoneParameter();
            callCenterPhoneParameter.Number = "8 (922) 123-45-67";

            var numberWithoutMarks = callCenterPhoneParameter.NumberWithoutMarks;
            Assert.Equal("89221234567", numberWithoutMarks);
        }
        
        [Fact]
        public void NumberWithoutMarksNullTest()
        {
            var callCenterPhoneParameter = new CallCenterPhoneParameter();

            var numberWithoutMarks = callCenterPhoneParameter.NumberWithoutMarks;
            Assert.Null(numberWithoutMarks);
        }
        
        [Fact]
        public void NumberWithoutMarksEmptyStringTest()
        {
            var callCenterPhoneParameter = new CallCenterPhoneParameter();
            callCenterPhoneParameter.Number = string.Empty;

            var numberWithoutMarks = callCenterPhoneParameter.NumberWithoutMarks;
            Assert.Equal("", numberWithoutMarks);
        }

        [Fact]
        public void GetIconUrlTest()
        {
            var callCenterPhoneParameter = new CallCenterPhoneParameter();
            callCenterPhoneParameter.IconPath = @"icons\2.png";
            var host = "http://dodopizza.com";
            var url = callCenterPhoneParameter.GetIconUrl(host);
                
            Assert.Equal("http://dodopizza.com/icons/2.png", url);
        }
        
        [Fact]
        public void GenerateNode()
        {
            var callCenterPhoneParameter = new CallCenterPhoneParameter();
            callCenterPhoneParameter.IconPath = @"icons\2.png";
            callCenterPhoneParameter.Number = "8 (902) 123-45-67";

            var node = callCenterPhoneParameter.CreateXmlNode();
            
            var expectedNode = new XElement("Phone",
                new XAttribute("number", "8 (902) 123-45-67"),
                new XAttribute("iconPath", @"icons\2.png"),
                new XAttribute("iconSitePath", ""));
            
            Assert.Equal(expectedNode.ToString(), node.ToString());
        }
    }
}