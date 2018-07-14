using System;
using System.Collections.Generic;
using System.Linq;
using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Stores;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
	public interface ITrackerClient
	{
		IProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit);
	    IProductionOrder[] GetOrders();
	    IProductionOrder[] GetExpiredOrders(DateTime now);


	}

	public class TrackerClient : ITrackerClient
	{
	    private readonly IOrdersStore _ordersStore;

	    public TrackerClient(IOrdersStore ordersStore)
	    {
	        _ordersStore = ordersStore;
	    }
	    public List<IProductionOrder> Orders { get; private set; } = new List<IProductionOrder>();

	    public void PlaceOrder(IProductionOrder order)
	    {
            _ordersStore.AddOrder(order);
	    }
	    public IProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
		{
			
			return _ordersStore.GetOrders()?.ToArray();
		}

	    public IProductionOrder[] GetOrders()
	    {
	        return  _ordersStore.GetOrders()?.ToArray();
	    }

	    public IProductionOrder[] GetExpiredOrders(DateTime now)
	    {
	        return _ordersStore.GetOrders()?.Where(o => o.IsExpired(now)).ToArray();
	    }
    }
}