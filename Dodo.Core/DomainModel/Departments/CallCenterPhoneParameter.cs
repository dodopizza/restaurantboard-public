using System;
using System.Linq;
using System.Xml.Linq;

namespace Dodo.Core.DomainModel.Departments
{
    [Serializable]
    public class CallCenterPhoneParameter
    {
        public String Number { get; set; }

        /// <summary>
        /// Путь на внешнем сервере (файл сервер)
        /// </summary>
        public String IconPath { set; get; }

        /// <summary>
        /// Путь в текущем каталоге сайта
        /// </summary>
        public String IconSitePath { set; get; }

        public String NumberWithoutMarks
        {
            get
            {
                var exceptChars = new[] { '-', '(', ')', ' ' };
                return new string(Number?.Where(x => !exceptChars.Contains(x)).ToArray());
            }
        }

        public String GetIconUrl(String host)
        {
            if (!String.IsNullOrEmpty(IconPath))
            {
                return (host.TrimEnd('/', '\\') + "/" + IconPath).Replace('\\', '/');
            }

            if (!String.IsNullOrEmpty(IconSitePath))
            {
                return IconSitePath;
            }

            return String.Empty;
        }



        public XElement CreateXmlNode()
        {
            return new XElement("Phone",
                new XAttribute("number", Number ?? String.Empty),
                new XAttribute("iconPath", IconPath ?? String.Empty),
                new XAttribute("iconSitePath", IconSitePath ?? String.Empty));
        }

        public static CallCenterPhoneParameter[] GetCallCenterPhonesFromXml(XElement container)
        {
            return container.Element("CallCenterPhones")?
                .Elements()
                .Select(CreateCallCenterParameter)
                .ToArray();
        }

        private static CallCenterPhoneParameter CreateCallCenterParameter(XElement element)
        {
            return new CallCenterPhoneParameter
            {
                Number = GetAttributeValue(element, "number"),
                IconPath = GetAttributeValue(element, "iconPath"),
                IconSitePath = GetAttributeValue(element, "iconSitePath")
            };
        }

        private static string GetAttributeValue(XElement element, string attributName)
        {
            return element.Attribute(attributName)?.Value ?? string.Empty;
        }
    }
}
