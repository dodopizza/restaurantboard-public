using System;
using System.Linq;
using System.Xml.Linq;

namespace Dodo.Core.DomainModel.Departments
{
	[Serializable]
	public class CallCenterPhone
	{
		private static readonly string[] ReplacedMarks = { "-", " ", "(", ")" };
		
		public String Number { get; set; }

		public String NumberWithoutMarks
		{
			get
			{
				if (String.IsNullOrEmpty(Number))
					return Number;

				return ReplacedMarks.Aggregate(Number, (current, mark) => current.Replace(mark, ""));
			}
		}

		public Icon Icon { get; set; }

		public XElement CreateXmlNode()
		{
			return CallCenterPhoneXmlConverter.CreateXmlNode(this);
		}
	}
}
