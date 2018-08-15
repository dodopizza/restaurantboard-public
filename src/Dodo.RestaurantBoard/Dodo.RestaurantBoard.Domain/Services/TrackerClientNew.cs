using System.Linq;
using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Repositories;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class TrackerClientNew
    {
        private readonly TrackerClient _oldTrackerClient;
		
        public TrackerClientNew(OrdersRepository repository)
        {
            _oldTrackerClient = new TrackerClient { OrdersRepository = repository };
        }			
		
        public virtual ProductionOrder[] GetOrders(int limit)
        {
            return _oldTrackerClient.GetOrdersByType(Uuid.NewUUId(), OrderType.Delivery, new OrderState[0], limit);
        }
		
        public virtual ProductionOrder[] GetSortedOrders(int limit)
        {
            return GetOrders(limit).OrderBy(o => o.ClientName).ToArray();
        }
    }
}