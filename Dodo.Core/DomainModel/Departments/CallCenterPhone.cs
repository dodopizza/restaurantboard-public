using System;
using System.Linq;
using System.Xml.Linq;

namespace Dodo.Core.DomainModel.Departments
{
	[Serializable]
	public class CallCenterPhone
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
				return GetUrlForIconPath(host);
			}

			if (!String.IsNullOrEmpty(IconSitePath))
			{
				return IconSitePath;
			}

			return String.Empty;
		}

		private string GetUrlForIconPath(string host)
		{
			return (host.TrimEnd('/', '\\') + "/" + IconPath).Replace('\\', '/');
		}

		public String NumberWithoutMarks
		{
			get
			{
				if (String.IsNullOrEmpty(Number))
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



		public XElement CreateXmlNode()
		{
			return new XElement("Phone",
				new XAttribute("number", Number ?? String.Empty),
				new XAttribute("iconPath", IconPath ?? String.Empty),
				new XAttribute("iconSitePath", IconSitePath ?? String.Empty));
		}

		public static CallCenterPhone[] GetCallCenterPhonesFromXml(XElement container)
		{
			var phns = container.Element("CallCenterPhones");
			if(phns==null)
				return new CallCenterPhone[0];

			var callCenterPhones = phns.Elements().Select(CreatePhoneFromXml);

			return callCenterPhones.ToArray();
		}

		private static CallCenterPhone CreatePhoneFromXml(XElement element)
		{
			var number = "";
			var numberAttribute = element.Attribute("number");
			if (numberAttribute != null)
				number = numberAttribute.Value;

			var iconPath = "";
			var iconPathAttribute = element.Attribute("iconPath");
			if (iconPathAttribute != null)
				iconPath = iconPathAttribute.Value;

			var iconSitePath = "";
			var iconSitePathAttribute = element.Attribute("iconSitePath");
			if (iconSitePathAttribute != null)
				iconSitePath = iconSitePathAttribute.Value;

			return new CallCenterPhone
			{
				Number = number,
				IconPath = iconPath,
				IconSitePath = iconSitePath
			};
		}
	}
}
