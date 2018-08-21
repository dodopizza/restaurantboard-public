﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.Core.AppServices
{
    public class UnitOrders
    {
        public Pizzeria Unit { get; set; }
        public List<RestaurantReadnessOrders> Orders { get; set; }
    }

    public class PizzeriaOrdersService
    {
        private readonly ITrackerClient _trackerClient;
        private readonly IDepartmentsStructureService _departmentsStructureService;

        public PizzeriaOrdersService(
            ITrackerClient trackerClient,
            IDepartmentsStructureService departmentsStructureService)
        {
            _trackerClient = trackerClient;
            _departmentsStructureService = departmentsStructureService;
        }

        public async Task<UnitOrders> GetUnitOrders(int unitId)
        {
            const int maxCountOrders = 16;

            var pizzeria = _departmentsStructureService.GetPizzeriaOrCache(unitId);
            var trackerOrders = await _trackerClient.GetOrdersByTypeAsync(pizzeria.Uuid, OrderType.Stationary, maxCountOrders);
            var orders = trackerOrders
                .Select(MapToRestaurantReadnessOrders);

            return new UnitOrders
            {
                Unit = pizzeria,
                Orders = orders.ToList()
            };
        }

        private static RestaurantReadnessOrders MapToRestaurantReadnessOrders(ProductionOrder order)
        {
            return new RestaurantReadnessOrders(
                order.Id,
                order.Number,
                order.ClientName,
                order.ChangeDate ?? DateTime.Now);
        }
    }
}