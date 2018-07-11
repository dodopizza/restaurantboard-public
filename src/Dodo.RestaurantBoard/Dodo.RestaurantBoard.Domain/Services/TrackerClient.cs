using Dodo.Core.Common;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public interface ITrackerClient
	{
		ProductionOrder[] GetOrders(Uuid unitUuid, OrderType type, OrderState[] states, int limit,
			bool isExpiring = false);
	}

	public class TrackerClient : ITrackerClient
	{
		public ProductionOrder[] GetOrders(Uuid unitUuid, OrderType type, OrderState[] states, int limit,
			bool isExpiring = false)
		{
			var orders = new[]
			{
				new ProductionOrder
				{
					Id = 55,
					Number = 3,
					ClientName = "Пупа"
				},
				new ProductionOrder
				{
					Id = 56,
					Number = 4,
					ClientName = "Лупа"
				},
			};

			return orders;
		}
	}
}