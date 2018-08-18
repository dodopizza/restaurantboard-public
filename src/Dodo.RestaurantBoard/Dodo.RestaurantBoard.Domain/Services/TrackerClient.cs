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
			var orders = GetOrdersByType_LagacyImpl(unitUuid, type, states, limit);
			
			return orders;
		}		

		public ProductionOrder[] GetSortedOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
		{
			var orders = GetOrdersByType_LagacyImpl(unitUuid, type, states, limit);
			
			orders = orders.OrderBy(o => o.ClientName).ToArray();
			
			return orders;
		}
		
		private ProductionOrder[] GetOrdersByType_LagacyImpl(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
		{
			var orders = OrdersRepository.GetOrders();

			orders = LimitOrders(orders, limit);

			return orders;
		}

		private ProductionOrder[] LimitOrders(ProductionOrder[] orders, int limit)
		{
			return orders.Take(limit).ToArray();
		}		
	}
}