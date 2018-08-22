using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Tests.DSL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class BoardsControllerTest
    {
        [Fact]
        public async Task GetOrderReadinessToStationary_ShouldReturnOrderForPupaClientName()
        {
            var departmentStructureService = Create.DepartmentsStructureService.WithPizzeria(Create.Pizzeria.Please()).Please();
            var trackerClient = Create.TrackerClient.WithOrderFrom("Пупа").Please();
            var pizzeriaOrdersService = Create.PizzeriaOrdersService.With(trackerClient).Please();
            var boardsController = new BoardsController(departmentStructureService, null, null, null, pizzeriaOrdersService);

            var result = await boardsController.GetOrderReadinessToStationary(unitId: 29);

            Assert.Equal("Пупа", GetClientName(result));
        }

        private string GetClientName(JsonResult result)
        {
            var clientOrder = (result.Value.GetType().GetProperty("ClientOrders").GetValue(result.Value) as IEnumerable<object>).Single();
            return clientOrder.GetType().GetProperty("ClientName").GetValue(clientOrder).ToString();
        }
    }
}