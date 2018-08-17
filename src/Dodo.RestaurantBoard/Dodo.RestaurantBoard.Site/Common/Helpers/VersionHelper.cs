using System;
using System.ComponentModel;
using System.Reflection;

namespace Dodo.RestaurantBoard.Site.Common.Helpers
{
	[Description("VersionHelper у каждой сборки должен быть свой, т.к. он зависит от Assembly")]
	public class VersionHelper
	{
		public const string VERSION_QUERY_PARAMETER = "v";

		private static readonly Version _version =
			Assembly.GetExecutingAssembly().GetName().Version;

		public string GetVersionToken() =>
			VERSION_QUERY_PARAMETER + "=" + _version.ToString(2);

		public static string AddVersionToken(string url)
		{
			var versionToken = new VersionHelper().GetVersionToken();
			var queryStart = url.IndexOf("?", StringComparison.Ordinal);
			url = queryStart == -1
				? url + "?" + versionToken
				: url.Insert(queryStart + 1, versionToken + "&");

			return url;
		}
	}
}
