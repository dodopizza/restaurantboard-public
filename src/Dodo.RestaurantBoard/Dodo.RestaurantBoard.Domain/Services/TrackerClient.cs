using Dodo.Core.Common;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using System.Linq;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public interface ITrackerClient
	{
		ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit);
        ProductionOrder[] GetOrdersByCount(Uuid unitUuid, OrderType type, OrderState[] states, int limit);

    }

	public class TrackerClient : ITrackerClient
	{
		public ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
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

        public ProductionOrder[] GetOrdersByCount(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
        {
            return GetOrdersByType(unitUuid, type, states, limit).Take(limit).ToArray();
        }
	}
}