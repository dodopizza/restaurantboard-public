using System;
using System.Collections.Generic;
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

		public CallCenterPhoneParameter[] GetCallCenterPhonesFromXml(XElement container)
		{
			var phns = container.Element("CallCenterPhones");
			if(phns==null)
				return new CallCenterPhoneParameter[0];

			var callCenterPhones = GetCallCenterPhonesParameters(phns);

			return callCenterPhones.ToArray();
		}

		public IEnumerable<CallCenterPhoneParameter> GetCallCenterPhonesParameters(XElement phns)
		{
			return phns.Elements().Select(x => new CallCenterPhoneParameter
			{
				Number = new XElementValueGetter(x).GetAttributeValueFrom("number"),
				IconPath = new XElementValueGetter(x).GetAttributeValueFrom("iconPath"),
				IconSitePath = new XElementValueGetter(x).GetAttributeValueFrom("iconSitePath")
			});
		}
	}


	public class XElementValueGetter
	{
		private readonly XElement _xElement;
	
		public XElementValueGetter(XElement xElement)
		{
			_xElement = xElement;
		}

		public string GetAttributeValueFrom(string attributeName)
		{
			var attribute = _xElement.Attribute(attributeName);
			return attribute?.Value ?? "";
		}
	}
}
