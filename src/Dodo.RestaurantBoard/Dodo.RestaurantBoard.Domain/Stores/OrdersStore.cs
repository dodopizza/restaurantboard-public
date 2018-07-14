using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dodo.Tracker.Contracts;

namespace Dodo.RestaurantBoard.Domain.Stores
{
    public interface IOrdersStore
    {
        void AddOrder(IProductionOrder order);
        List<IProductionOrder> GetOrders();
        List<IProductionOrder> GetExpiredOrders(DateTime now);
    }

    public class OrdersStore : IOrdersStore
    {
        private readonly List<IProductionOrder> _orders = new List<IProductionOrder>();

        public void AddOrder(IProductionOrder order)
        {
            _orders.Add(order);
        }

        public List<IProductionOrder> GetOrders()
        {
            return _orders;
        }

        public List<IProductionOrder> GetExpiredOrders(DateTime now)
        {
            return _orders.Where(o=>o.IsExpired(now)).ToList();
        }
    }
}
