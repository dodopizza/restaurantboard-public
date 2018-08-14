using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace Dodo.RestaurantBoard.Site.Controllers {
    public class LocalizedContext
    {
        private const string _localizedResourcesFolder = "LocalizedResources";

        public static string LocalizedContent(IHostingEnvironment hostingEnvironment, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
                throw new ArgumentNullException(nameof (contentPath));
            StringBuilder stringBuilder = new StringBuilder();
            CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
            int serverPathLength = hostingEnvironment.WebRootPath.Length - 1;
            contentPath = contentPath.Trim('/');
            string path1 = GetLocalizedPathWithCulture(hostingEnvironment.WebRootPath, contentPath, currentUiCulture);
            if (File.Exists(path1))
                return ConvertLocalPathToRelativeUrl(path1, serverPathLength);
            stringBuilder.AppendLine(path1);
            string path2 = GetLocalizedPath(hostingEnvironment.WebRootPath, contentPath);
            if (File.Exists(path2))
                return ConvertLocalPathToRelativeUrl(path2, serverPathLength);
            stringBuilder.AppendLine(path2);
            throw new FileNotFoundException(stringBuilder.ToString());
        }

        private static string ConvertLocalPathToRelativeUrl(string path, int serverPathLength)
        {
            return path.Substring(serverPathLength).Replace('\\', '/');
        }

        public static string GetLocalizedPathWithCulture(string webRootPath, string contentPath, CultureInfo culture)
        {
            return new LocalizedContext().GetLocalizedPathWithCultureNew(webRootPath, contentPath, culture);
        }

        public string GetLocalizedPathWithCultureNew(string webRootPath, string contentPath, CultureInfo culture)
        {
            return webRootPath + "/" + Path.Combine("LocalizedResources", culture.TwoLetterISOLanguageName, contentPath);
        }

        public static string GetLocalizedPath(string webRootPath, string contentPath)
        {
            return webRootPath + "/" + Path.Combine("LocalizedResources", contentPath);
        }
    }
}