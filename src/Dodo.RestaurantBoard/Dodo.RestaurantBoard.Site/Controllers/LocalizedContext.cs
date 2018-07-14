using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using Dodo.RestaurantBoard.Domain.Services;
using Microsoft.AspNetCore.Hosting;

namespace Dodo.RestaurantBoard.Site.Controllers
{
    public static class LocalizedContext
    {
        private const string _localizedResourcesFolder = "LocalizedResources";

        public static string LocalizedContent(IHostingEnvironment hostingEnvironment, IFileService fileService, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
                throw new ArgumentNullException(nameof(contentPath));
            StringBuilder stringBuilder = new StringBuilder();
            CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
            int serverPathLength = hostingEnvironment.WebRootPath.Length - 1;
            contentPath = contentPath.Trim('/');
            string path1 = hostingEnvironment.WebRootPath + "/" + Path.Combine("LocalizedResources", currentUiCulture.TwoLetterISOLanguageName, contentPath);
            if (fileService.Exists(path1))
                return ConvertLocalPathToRelativeUrl(path1, serverPathLength);
            stringBuilder.AppendLine(path1);
            string path2 = hostingEnvironment.WebRootPath + "/" + Path.Combine("LocalizedResources", contentPath);
            if (fileService.Exists(path2))
                return ConvertLocalPathToRelativeUrl(path2, serverPathLength);
            stringBuilder.AppendLine(path2);
            throw new FileNotFoundException(stringBuilder.ToString());
        }

        private static string ConvertLocalPathToRelativeUrl(string path, int serverPathLength)
        {
            return path.Substring(serverPathLength).Replace('\\', '/');
        }
    }
}