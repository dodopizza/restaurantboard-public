using Dodo.Core.Common;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using System;
using System.Linq;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public interface ITrackerClient
	{
		ProductionOrder[] GetOrders(Uuid unitUuid, OrderType type, OrderState[] states, int limit,
			bool expiringOnly = false);
	}

	public class TrackerClient : ITrackerClient
	{
        private readonly IOrdersProvider _ordersProvider;
		private readonly IDateProvider _dateProvider;

        public TrackerClient(IOrdersProvider ordersProvider, IDateProvider dateProvider)
        {
            _ordersProvider = ordersProvider;
	        _dateProvider = dateProvider;
        }

		public ProductionOrder[] GetOrders(Uuid unitUuid, OrderType type, OrderState[] states, int limit,
			bool expiringOnly = false)
		{
			var orders = _ordersProvider.GetOrders();

			if (!expiringOnly) return orders;
			
			var now = _dateProvider.Now();
			return orders.Where(order => order.IsExpiring(now)).ToArray();
		}
	}
}