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
            var random = new Random(DateTime.Now.Millisecond);
            
            return new[]
            {
                new ProductionOrder
                {
                    Id = 55,
                    Number = 3,
                    ClientName = "Пупа",
                    ChangeDate = DateTime.Now.AddMinutes(-random.Next(0, 120))
                },
                new ProductionOrder
                {
                    Id = 56,
                    Number = 4,
                    ClientName = "Лупа",
                    ChangeDate = DateTime.Now.AddMinutes(-random.Next(0, 120))
                },
                new ProductionOrder
                {
                    Id = 1,
                    Number = 3,
                    ClientName = "Миша",
                    ChangeDate = DateTime.Now.AddMinutes(-random.Next(0, 120))
                },
                new ProductionOrder
                {
                    Id = 2,
                    Number = 4,
                    ClientName = "Таня",
                    ChangeDate = DateTime.Now.AddMinutes(-random.Next(0, 120))
                },
            };
        }
    }
}
