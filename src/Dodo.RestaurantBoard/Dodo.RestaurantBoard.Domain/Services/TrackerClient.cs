using System;
using System.Net.Http;
using System.Threading.Tasks;
using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Newtonsoft.Json;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public class TrackerClient : ITrackerClient
	{
		private readonly HttpClient _client;

		public TrackerClient(Uri baseUri)
		{
			_client = new HttpClient
			{
				BaseAddress = baseUri
			};
		}

		public async Task<ProductionOrder[]> GetOrdersByTypeAsync(
			Uuid unitUuid,
			OrderType type,
			int limit)
		{
			var url = $"api/orders/unit/{unitUuid}?type={type}&limit={limit}";
			var json = await _client.GetStringAsync(url);

			return JsonConvert.DeserializeObject<ProductionOrder[]>(json);
		}
	}
}