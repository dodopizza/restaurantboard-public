using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public interface ITrackerClient
	{
		ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit);
        ProductionOrder[] GetAllOrders();

    }

    public class TrackerClient : ITrackerClient
    {
        private readonly IOrdersStorage ordersStorage;

        public TrackerClient(IOrdersStorage ordersStorage)
        {
            this.ordersStorage = ordersStorage;

            ordersStorage.AddProductionOrder("Пупа", 3);
            ordersStorage.AddProductionOrder("Лупа", 4);
        }

        public ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
        {
            return ordersStorage.GetAllProductionOrders();
        }

        public ProductionOrder[] GetAllOrders()
        {
            return ordersStorage.GetAllProductionOrders();
        }
    }
}