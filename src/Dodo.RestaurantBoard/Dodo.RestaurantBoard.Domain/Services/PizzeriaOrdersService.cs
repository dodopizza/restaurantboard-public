using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class PizzeriaOrdersService : IPizzeriaOrdersService
    {
        private readonly ITrackerClient _trackerClient;

        public PizzeriaOrdersService(ITrackerClient trackerClient)
        {
            _trackerClient = trackerClient;
        }

        public async Task<RestaurantReadnessOrders[]> GetOrders(Pizzeria pizzeria)
        {
            const int maxCountOrders = 16;

            return (await _trackerClient
                    .GetOrdersByTypeAsync(pizzeria.Uuid, OrderType.Stationary, maxCountOrders))
                .Select(MapToRestaurantReadnessOrders)
                .ToArray();
        }

        private RestaurantReadnessOrders MapToRestaurantReadnessOrders(ProductionOrder order)
        {
            return new RestaurantReadnessOrders(order.Id, order.Number, order.ClientName, order.ChangeDate ?? DateTime.Now);
        }
    }
}
