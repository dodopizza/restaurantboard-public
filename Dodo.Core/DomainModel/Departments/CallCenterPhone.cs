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


		public String NumberWithoutMarks
		{
			get
			{
                if (CheckNumberIsNullOrEmpty(Number))
                    return Number;

				String[] replacedMarks = { "-", " ", "(", ")" };
				String replacedNumber = Number;
				foreach (String mark in replacedMarks)
				{
					replacedNumber = replacedNumber.Replace(mark, "");
				}
				return replacedNumber;
			}
		}

        public bool CheckNumberIsNullOrEmpty(string number)
        {
            return (String.IsNullOrEmpty(number));
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
			var phns = container.Element("CallCenterPhones");
			if(phns==null)
				return new CallCenterPhoneParameter[0];

			var callCenterPhones = phns.Elements().Select(x =>
			{
				string number = new AttributeGetter("number").GetAttribute(x);

				string iconPath = new AttributeGetter("iconPath").GetAttribute(x);

				string iconSitePath = new AttributeGetter("iconSitePath").GetAttribute(x);

				return new CallCenterPhoneParameter
				{
					Number = number,
					IconPath = iconPath,
					IconSitePath = iconSitePath
				};
			});

			return callCenterPhones.ToArray();
		}
	}

    public class AttributeGetter
    {
        private string _attributeName;

        public AttributeGetter(string attributeName)
        {
            _attributeName = attributeName;
        }

        public string GetAttribute(XElement element)
        {
            var attribute = element.Attribute(_attributeName);
            if (attribute != null)
                return attribute.Value;
            else
                return "";
        }
    }
}
