using System;
using System.ComponentModel;
using System.Reflection;

namespace Dodo.RestaurantBoard.Site.Common.Helpers
{
    [Description("VersionHelper у каждой сборки должен быть свой, т.к. он зависит от Assembly")]
    public class VersionHelper
    {
        public const string VERSION_QUERY_PARAMETER = "v";

        public string GetVersionToken() =>
            VERSION_QUERY_PARAMETER + "=" + GetVersion().ToString(2);

        protected virtual Version GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version;
        }

        public string AddVersionToken(string url)
        {
            var versionToken = GetVersionToken();
            url = FormatUrl(url, versionToken);

            return url;
        }

        public static string FormatUrl(string url, string versionToken)
        {
            var queryStart = url.IndexOf("?", StringComparison.Ordinal);
            url = queryStart == -1
                ? url + "?" + versionToken
                : url.Insert(queryStart + 1, versionToken + "&");
            return url;
        }
    }
}
