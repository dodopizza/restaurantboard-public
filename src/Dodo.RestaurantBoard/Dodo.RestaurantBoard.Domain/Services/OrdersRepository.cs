using System;
using System.Collections.Generic;
using System.Linq;
using Dodo.Tracker.Contracts;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly List<ProductionOrder> _orders;

        public OrdersRepository()
        {
            _orders = new List<ProductionOrder>
            {
                new ProductionOrder
                {
                    Id = 55,
                    Number = 3,
                    ClientName = "Пупа"
                },
                new ProductionOrder
                {
                    Id = 56,
                    Number = 4,
                    ClientName = "Лупа"
                },
            };
        }

        public IEnumerable<ProductionOrder> GetOrders()
        {
            return _orders;
        }

        public void AddOrder(ProductionOrder order)
        {
            if (order == null)
                throw new ArgumentException(nameof(order));

            order.Id = _orders.Any()
                ? _orders.Max(x => x.Id) + 1
                : 1;

            _orders.Add(order);
        }

        public void DeleteOrder(int id)
        {
            var order = _orders.FirstOrDefault(p => p.Id == id);

            if (order != null)
            {
                _orders.Remove(order);
            }
        }

        public ProductionOrder GetOrder(int id)
        {
            return _orders.FirstOrDefault(x => x.Id == id);
        }
    }
}
