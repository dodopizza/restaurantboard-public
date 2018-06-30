using Dodo.Core.Common;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class CustomTrackerClient : ITrackerClient
    {
        public ProductionOrder[] GetOrdersByType(Uuid unitUuid, OrderType type, OrderState[] states, int limit)
        {
            var orders = new[]
            {
                new ProductionOrder
                {
                    Id = 57,
                    Number = 10,
                    ClientName = "Таня"
                },
                new ProductionOrder
                {
                    Id = 58,
                    Number = 20,
                    ClientName = "Женя"
                },
            };

            return orders;
        }
    }
}
