using System.Xml.Linq;

namespace Dodo.Core.DomainModel.Departments.Extensions
{
    public static class XElementExtensions
    {
        public static string GetAttributeValueByName(this XElement xElement, string name)
        {
            return xElement.Attribute(name)?.Value;
        }
        
    }
}