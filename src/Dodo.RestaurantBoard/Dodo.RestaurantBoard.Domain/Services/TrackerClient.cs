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
        private IOrdersProvider _ordersProvider;

        public TrackerClient(IOrdersProvider ordersProvider)
        {
            _ordersProvider = ordersProvider;
        }

		public ProductionOrder[] GetOrders(Uuid unitUuid, OrderType type, OrderState[] states, int limit,
			bool isExpiring = false)
		{
            var orders = _ordersProvider.GetOrders();

            //todo

            return orders;
		}
	}
}