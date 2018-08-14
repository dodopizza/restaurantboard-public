using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;

namespace Dodo.RestaurantBoard.Site.Controllers
{
    public class LocalizedContextNew
    {
        private const string _localizedResourcesFolder = "LocalizedResources";

        public string LocalizedContent(string webRootPath, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
                throw new ArgumentNullException(nameof(contentPath));
            StringBuilder stringBuilder = new StringBuilder();
            CultureInfo currentUiCulture = Thread.CurrentThread.CurrentUICulture;
            int serverPathLength = webRootPath.Length - 1;
            contentPath = contentPath.Trim('/');
            string path1 = webRootPath + "/" + Path.Combine("LocalizedResources", currentUiCulture.TwoLetterISOLanguageName, contentPath);
            if (File.Exists(path1))
                return ConvertLocalPathToRelativeUrl(path1, serverPathLength);
            stringBuilder.AppendLine(path1);
            string path2 = webRootPath + "/" + Path.Combine("LocalizedResources", contentPath);
            if (File.Exists(path2))
                return ConvertLocalPathToRelativeUrl(path2, serverPathLength);
            stringBuilder.AppendLine(path2);
            throw new FileNotFoundException(stringBuilder.ToString());
        }

        private string ConvertLocalPathToRelativeUrl(string path, int serverPathLength)
        {
            return path.Substring(serverPathLength).Replace('\\', '/');
        }
    }
}