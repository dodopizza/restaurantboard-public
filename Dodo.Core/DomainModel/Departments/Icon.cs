using System;

namespace Dodo.Core.DomainModel.Departments
{
	public class Icon
	{
		/// <summary>
		/// Путь на внешнем сервере (файл сервер)
		/// </summary>
		public string Path { set; get; }

		/// <summary>
		/// Путь в текущем каталоге сайта
		/// </summary>
		public string SitePath { set; get; }

		public Icon(string path, string sitePath)
		{
			Path = path;
			SitePath = sitePath;
		}

		public string GetUrl(string host)
		{
			if (!string.IsNullOrEmpty(Path))
			{
				return (host.TrimEnd('/', '\\') + "/" + Path).Replace('\\', '/');
			}

			if (!string.IsNullOrEmpty(SitePath))
			{
				return SitePath;
			}

			return string.Empty;
		}
	}
}