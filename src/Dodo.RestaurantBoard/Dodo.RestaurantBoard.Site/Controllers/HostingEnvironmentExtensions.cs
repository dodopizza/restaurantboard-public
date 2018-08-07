using System;
using System.IO;
using System.Threading;
using Microsoft.AspNetCore.Hosting;

namespace Dodo.RestaurantBoard.Site.Controllers {
    public static class HostingEnvironmentExtensions
    {
        private const string _localizedResourcesFolder = "LocalizedResources";

        public static string LocalizedContent(this IHostingEnvironment hostingEnvironment, string contentPath)
        {
            if (string.IsNullOrEmpty(contentPath))
            {
                throw new ArgumentNullException(nameof(contentPath));
            }

            contentPath = contentPath.Trim('/');

            var localizedPath = hostingEnvironment.GetResourcePath(contentPath, true);
            var globalPath = hostingEnvironment.GetResourcePath(contentPath, false);

            foreach (var path in new[] { localizedPath, globalPath })
            {
                if (File.Exists(path))
                {
                    return ConvertLocalPathToRelativeUrl(path, hostingEnvironment.WebRootPath);
                }
            }

            throw new FileNotFoundException($"{localizedPath}{Environment.NewLine}{globalPath}");
        }

        private static string ConvertLocalPathToRelativeUrl(string path, string webrootpath)
        {
            var serverPathLength = webrootpath.Length - 1;
            return path.Substring(serverPathLength).Replace('\\', '/');
        }

        private static string GetResourcePath(this IHostingEnvironment hostingEnvironment, string contentPath, bool localized)
        {
            return hostingEnvironment.WebRootPath + "/" + (localized 
                ? Path.Combine(_localizedResourcesFolder, Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName, contentPath)
                : Path.Combine(_localizedResourcesFolder, contentPath));
        }
    }
}