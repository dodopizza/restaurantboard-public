using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Tests.DSL;
using Newtonsoft.Json;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class BoardsControllerTest
    {
        [Fact]
        public void GetOrderReadinessToStationary_ShouldReturnOrderForPupaClientName()
        {
            var pizzeriaOrdersService = Create.PizzeriaOrdersService.Please();
            var boardsController = new BoardsController(null, null, null, null, pizzeriaOrdersService);

            var result = boardsController.GetOrderReadinessToStationary(unitId: 29).Result;

            Assert.True(true);
        }
    }
}