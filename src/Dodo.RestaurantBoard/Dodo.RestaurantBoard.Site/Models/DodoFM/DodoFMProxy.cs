using System;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace Dodo.RestaurantBoard.Site.Models.DodoFM
{
	public class DodoFMProxy
	{
		private const string DODO_FM_KEY = "DodoFMSong";

		public static string GetSongName()
		{
			var songName = MemoryCache.Default.Get(DODO_FM_KEY) as string;
			if (!string.IsNullOrWhiteSpace(songName))
				return songName;

			var requestUrl = WebConfigurationManager.AppSettings["DodoFMApiUrl"];
			if (string.IsNullOrWhiteSpace(requestUrl))
				return songName;

			try
			{
				using (var client = new WebClient())
				{
					client.Headers.Clear();
					client.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
					var jsonString = client.DownloadString(requestUrl);

					var data = JsonConvert.DeserializeObject<DodoFMData>(jsonString);
					if (data == null || data.Objects.Length <= 0)
						return songName;

					songName = data.Objects.First().Metadata;

					var policy = new CacheItemPolicy();
					policy.AbsoluteExpiration = DateTime.Now.AddSeconds(15);

					MemoryCache.Default.Add(DODO_FM_KEY, songName, policy);
				}
			}
			catch
			{
				return string.Empty;
			}

			return songName;
		}
	}
}
