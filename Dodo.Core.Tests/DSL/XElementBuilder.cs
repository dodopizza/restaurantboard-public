using System;
using System.Xml.Linq;

namespace Dodo.Core.Tests.DSL
{
    public class XElementBuilder
    {
        private readonly XElement _xElement;

        public XElementBuilder(string name)
        {
            _xElement = new XElement(name);
        }

        public XElementBuilder WithNumber(string number)
        {
            return With(nameof(number), number);
        }

        public XElementBuilder WithIconPath(string iconPath)
        {
            return With(nameof(iconPath), iconPath);
        }

        public XElementBuilder WithIconSitePath(string iconSitePath)
        {
            return With(nameof(iconSitePath), iconSitePath);
        }

        public XElement Please()
        {
            return _xElement;
        }

        private XElementBuilder With(string attributeName, string attributeValue)
        {
            _xElement.Add(new XAttribute(attributeName, attributeValue ?? String.Empty));
            return this;
        }

        public XElementBuilder WithName(string name)
        {
            _xElement.SetAttributeValue(nameof(name), name);
            return this;
        }
    }
}