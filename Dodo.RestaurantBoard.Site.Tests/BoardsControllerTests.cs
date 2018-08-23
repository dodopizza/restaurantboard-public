using System;
using System.Collections.Generic;
using System.Linq;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Site.Tests.DSL;
using Dodo.Tracker.Contracts;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class BoardsControllerTests
    {
        [Fact]
        public async void GetOrderReadinessToStationary_ShouldReturnAmountOfOrdersEqualsToReturnedFromTrackerClient()
        {
            var departmentStructureServiceStub = Create
                .DepartmentsStructureServiceBuilder
                .With(pizzeria: Create.PizzeriaBuilder.With(id: 1).Please())
                .Please();
            var clientsServiceStub = Create
                .ClientsServiceBuilder
                .WithoutIcons()
                .Please();
            var trackerClientStub = Create
                .TrackerClientBuilder
                .With(Orders())
                .Please();
            var boardsController = new BoardsController(departmentStructureServiceStub, clientsServiceStub, null, trackerClientStub, null);

            var result = await boardsController.GetOrderReadinessToStationary(1);

            Assert.Equal(2, GetOrdersCount(result));
        }

        private int GetOrdersCount(JsonResult result)
        {
            var orders = (result.Value.GetType().GetProperty("ClientOrders").GetValue(result.Value) as IEnumerable<object>);
            return orders?.Count() ?? 0;
        }

        private ProductionOrder[] Orders()
        {
            return new []
            {
                new ProductionOrder
                {
                    Id = 55,
                    Number = 3,
                    ClientName = "Пупа",
                    ChangeDate = DateTime.Now.AddMinutes(-5),
                },

                new ProductionOrder
                {
                    Id = 56,
                    Number = 4,
                    ClientName = "Лупа",
                    ChangeDate = DateTime.Now.AddMinutes(-3),
                }
            };
        }
    }
}
