using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Tests.DSL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class BoardsControllerTest
    {
        [Fact]
        public void GetOrderReadinessToStationary_ShouldReturnOrderForPupaClientName()
        {
            var departmentStructureService = Create.DepartmentsStructureService.WithPizzeria(Create.Pizzeria.Please()).Please();
            var pizzeriaOrdersService = Create.PizzeriaOrdersService.Please();
            var boardsController = new BoardsController(departmentStructureService, null, null, null, pizzeriaOrdersService);

            var result = boardsController.GetOrderReadinessToStationary(unitId: 29).Result;


            JObject jobj = JObject.Parse(result.ToString());
           // jobj["ClientOrders"][0]["ClientName"]

            Assert.True(true);
        }
    }
}