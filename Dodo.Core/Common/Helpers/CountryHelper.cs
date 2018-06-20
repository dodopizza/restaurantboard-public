using System;
using System.Configuration;
using Dodo.Core.Common.Enums;

namespace Dodo.Core.Common.Helpers
{
	public class CountryHelper
	{
		public static CountryCode GetCountryCodeOrDefault => ConfigurationManager.AppSettings["CurrentCountryId"] != null
																	? (CountryCode)Convert.ToInt32(ConfigurationManager.AppSettings["CurrentCountryId"])
																	: CountryCode.Ru;
	}
}
