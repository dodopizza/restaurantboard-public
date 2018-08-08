﻿using System;
using System.Linq;
using System.Xml.Linq;

namespace Dodo.Core.DomainModel.Departments
{
	public static class CallCenterPhoneXmlConverter
	{
		public static XElement CreateXmlNode(CallCenterPhoneParameter phone)
		{
			return new XElement("Phone",
				new XAttribute("number", phone.Number ?? String.Empty),
				new XAttribute("iconPath", phone.IconPath ?? String.Empty),
				new XAttribute("iconSitePath", phone.IconSitePath ?? String.Empty));
		}

		public static CallCenterPhoneParameter[] GetCallCenterPhones(this XElement container)
		{
			var phns = container.Element("CallCenterPhones");
			if(phns==null)
				return new CallCenterPhoneParameter[0];

			return phns.Elements().Select(ParseCallCenterPhone).ToArray();
		}

		private static CallCenterPhoneParameter ParseCallCenterPhone(XElement element)
		{
			return new CallCenterPhoneParameter
			{
				Number = GetElement(element, "number"),
				IconPath = GetElement(element, "iconPath"),
				IconSitePath = GetElement(element, "iconSitePath")
			};
		}

		private static string GetElement(XElement element, string attribute)
		{
			return element.Attribute(attribute)?.Value ?? "";
		}
	}
}