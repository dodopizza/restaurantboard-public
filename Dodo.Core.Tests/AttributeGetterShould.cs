using Dodo.Core.DomainModel.Departments;
using System.Xml.Linq;
using Xunit;

namespace Dodo.Core.Tests
{
    public class AttributeGetterShould
    {
        [Fact]
        public void ReturnAttributeValueIfItExists()
        {
            var attribute = new XAttribute("attributeName", "attributeValue");
            var xElement = new XElement("elementName", attribute);
            var attributeGetter = new AttributeGetter("attributeName");

            var attributeValue = attributeGetter.GetAttribute(xElement);

            Assert.Equal("attributeValue", attributeValue);
        }

        [Fact]
        public void ReturnEmptyStringIfAttributeNotExists()
        {
            var xElement = new XElement("elementName");
            var attributeGetter = new AttributeGetter("absentAttribute");

            var attributeValue = attributeGetter.GetAttribute(xElement);

            Assert.Equal("", attributeValue);
        }
    }
}
