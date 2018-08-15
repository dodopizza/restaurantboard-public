using System.Linq;
using Dodo.Core.Common;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Dodo.RestaurantBoard.Domain.Repositories;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public interface ITrackerClient
	{
		ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit);
		ProductionOrder[] GetSortedOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit);
	}

	public class TrackerClient : ITrackerClient
	{
		public OrdersRepository OrdersRepository = new OrdersRepository();
		
		public virtual ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
		{
			var orders = OrdersRepository.GetOrders();

			orders = LimitOrders(orders, limit);

			return orders;
		}

		public ProductionOrder[] GetSortedOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
		{
			var orders = this.GetOrdersByType(unitUuid, type, states, limit);

			return orders.OrderBy(o => o.ClientName).ToArray();
		}

		private ProductionOrder[] LimitOrders(ProductionOrder[] orders, int limit)
		{
			return orders.Take(limit).ToArray();
		}		
	}
}