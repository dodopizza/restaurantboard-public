using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.DomainModel.Pizzeria;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class PizzeriaOrdersService : IPizzeriaOrdersService
    {
        private readonly IDepartmentsStructureService departmentsStructureService;
        private readonly ITrackerClient trackerClient;

        public PizzeriaOrdersService(IDepartmentsStructureService departmentsStructureService, ITrackerClient trackerClient)
        {
            this.departmentsStructureService = departmentsStructureService;
            this.trackerClient = trackerClient;
        }

        public async Task<PizzeriaOrders> GetOrdersByUnitIdAsync(int unitId)
        {
            const int maxCountOrders = 16;

            var pizzeria = departmentsStructureService.GetPizzeriaOrCache(unitId);

            var orders = (await trackerClient
                    .GetOrdersByTypeAsync(pizzeria.Uuid, OrderType.Stationary, maxCountOrders))
                .Select(MapToRestaurantReadnessOrders)
                .ToArray();

            return new PizzeriaOrders
            {
                Pizzeria = pizzeria,
                Orders = orders
            };
        }

        private static RestaurantReadnessOrders MapToRestaurantReadnessOrders(ProductionOrder order)
        {
            return new RestaurantReadnessOrders(order.Id, order.Number, order.ClientName, order.ChangeDate ?? DateTime.Now);
        }
    }
    
}
