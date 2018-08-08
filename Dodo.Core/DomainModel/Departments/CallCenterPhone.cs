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

				return ReplacedMarks.Aggregate(Number, (current, mark) => current.Replace(mark, ""));
			}
		}

		public XElement CreateXmlNode()
		{
			return CallCenterPhoneXmlConverter.CreateXmlNode(this);
		}
	}
}
