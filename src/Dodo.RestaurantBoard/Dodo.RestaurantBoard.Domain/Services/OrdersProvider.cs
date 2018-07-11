using Dodo.Tracker.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public interface IOrdersProvider
    {
        ProductionOrder[] GetOrders();
    }

    public class OrdersProvider : IOrdersProvider
    {
        public ProductionOrder[] GetOrders()
        {
            return new[]
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
    }
}
