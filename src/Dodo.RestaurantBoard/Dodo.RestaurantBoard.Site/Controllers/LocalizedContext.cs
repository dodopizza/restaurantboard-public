using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Dodo.RestaurantBoard.Site.Controllers {
    public static class LocalizedContext
    {
        private const string _localizedResourcesFolder = "LocalizedResources";

        public static string LocalizedContent(HttpServerUtilityBase server, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
                throw new ArgumentNullException(nameof (contentPath));
            StringBuilder stringBuilder = new StringBuilder();
            CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
            int serverPathLength = server.MapPath("~").Length - 1;
            contentPath = contentPath.Trim('/');
            string path1 = server.MapPath("/" + Path.Combine("LocalizedResources", currentUiCulture.TwoLetterISOLanguageName, contentPath));
            if (File.Exists(path1))
                return ConvertLocalPathToRelativeUrl(path1, serverPathLength);
            stringBuilder.AppendLine(path1);
            string path2 = server.MapPath("/" + Path.Combine("LocalizedResources", contentPath));
            if (File.Exists(path2))
                return ConvertLocalPathToRelativeUrl(path2, serverPathLength);
            stringBuilder.AppendLine(path2);
            throw new FileNotFoundException(stringBuilder.ToString());
        }

        private static string ConvertLocalPathToRelativeUrl(string path, int serverPathLength)
        {
            return path.Substring(serverPathLength).Replace('\\', '/');
        }
    }

    public static class UrlHelperExtension
    {
        public static string ToAbsolute(this UrlHelper urlHelper, string relativePath)
        {
            return new Uri(new Uri(urlHelper.RequestContext.HttpContext.Request.Url.GetLeftPart(UriPartial.Authority), UriKind.Absolute), VirtualPathUtility.ToAbsolute(relativePath)).AbsoluteUri;
        }

        public static string LocalizedContent(this UrlHelper urlHelper, string contentPath)
        {
            return LocalizedContext.LocalizedContent(urlHelper.RequestContext.HttpContext.Server, contentPath);
        }
    }

}