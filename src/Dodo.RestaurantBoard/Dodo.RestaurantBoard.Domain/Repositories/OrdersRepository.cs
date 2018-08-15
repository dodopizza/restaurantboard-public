using Dodo.Tracker.Contracts;

namespace Dodo.RestaurantBoard.Domain.Repositories
{
    public class OrdersRepository
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