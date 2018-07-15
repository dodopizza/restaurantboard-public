using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using System;
using System.Linq;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public interface ITrackerClient
	{
		ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit);
        ProductionOrder[] GetAllOrders();

        ProductionOrder[] GetOrdersAfterDate(DateTime dateTime);

        ProductionOrder GetOrderByName(string clientName);
        void AddProductionOrder(string clientName, int number);

    }

    public class TrackerClient : ITrackerClient
    {
        private readonly IOrdersStorage ordersStorage;

        public TrackerClient(IOrdersStorage ordersStorage)
        {
            this.ordersStorage = ordersStorage;
            
        }

        public ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
        {
            return ordersStorage.GetAllProductionOrders().ToArray();
        }

        public ProductionOrder[] GetAllOrders()
        {
            return ordersStorage.GetAllProductionOrders().ToArray();
        }

        public void AddProductionOrder(string clientName, int number)
        {
            var existingOrder =ordersStorage.GetProductionOrderByName(clientName);

            if (existingOrder == null)
            {
                ordersStorage.AddProductionOrder(clientName, number);
            }
            else
            {
                ordersStorage.UpdateProductionOrderNumber(existingOrder.Id, existingOrder.Number + number);
            }
        }

        public ProductionOrder GetOrderByName(string clientName)
        {
            return ordersStorage.GetProductionOrderByName(clientName);
        }

        public ProductionOrder[] GetOrdersAfterDate(DateTime dateTime)
        {
            return ordersStorage.GetAllProductionOrders().Where(order => order.ChangeDate >= dateTime).ToArray();
        }
    }
}