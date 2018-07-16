using System;
using System.Xml.Linq;
using Xunit;
using Dodo.Core.DomainModel.Departments;

namespace Dodo.Core.UnitTests
{
    public class DepartmentShould
    {
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