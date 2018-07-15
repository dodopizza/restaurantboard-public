using System;
using System.Linq;
using Dodo.Core.Common;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public interface ITrackerClient
	{
		ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit);
	    void AddOrder(ProductionOrder order);
	    void DeleteOrder(int id);
	}

	public class TrackerClient : ITrackerClient
	{
        private IOrdersRepository ordersRepository;

        public TrackerClient(IOrdersRepository ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
		{
			return ordersRepository.GetOrders().Take(limit).ToArray();
		}

	    public void AddOrder(ProductionOrder order)
	    {
	        if (string.IsNullOrEmpty(order.ClientName))
                throw new ArgumentNullException($"{nameof(order.ClientName)} can not be null");

            ordersRepository.AddOrder(order);
	    }

	    public void DeleteOrder(int id)
	    {
	        if (ordersRepository.GetOrder(id) == null)
                throw new Exception("Entry not found");

	        ordersRepository.DeleteOrder(id);
	    }
	}
}