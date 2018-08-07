using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace Dodo.RestaurantBoard.Site.Controllers {
    public static class LocalizedContext
    {
        public static string LocalizedContent(IHostingEnvironment hostingEnvironment, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
            {
                throw new ArgumentNullException(nameof(contentPath));
            }
                
            var stringBuilder = new StringBuilder();
            var currentUiCulture = Thread.CurrentThread.CurrentUICulture;

            var serverPathLength = hostingEnvironment.WebRootPath.Length - 1;

            contentPath = contentPath.Trim('/');
            var path1 = hostingEnvironment.WebRootPath + "/"
                        + Path.Combine("LocalizedResources", currentUiCulture.TwoLetterISOLanguageName, contentPath);

            if (File.Exists(path1))
            {
                return ConvertLocalPathToRelativeUrl(path1, serverPathLength);
            }

            stringBuilder.AppendLine(path1);
            var path2 = hostingEnvironment.WebRootPath + "/" + Path.Combine("LocalizedResources", contentPath);
            if (File.Exists(path2))
            {
                return ConvertLocalPathToRelativeUrl(path2, serverPathLength);
            }

            stringBuilder.AppendLine(path2);
            throw new FileNotFoundException(stringBuilder.ToString());
        }

        private static string ConvertLocalPathToRelativeUrl(string path, int serverPathLength)
        {
            return path.Substring(serverPathLength).Replace('\\', '/');
        }
    }
}