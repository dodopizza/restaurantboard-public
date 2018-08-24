using Dodo.RestaurantBoard.Domain.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Dodo.Tests.DSL;

namespace Dodo.Tests
{
    public class BoardsControllerTests
    {
        [Fact]
        public async Task ShouldReturnOrder_WithLupaName()
        {
            var clientName = "Лупа";
            var pizzeriaOrder = Create.PizzeriaOrder().With(clientName).Please();
            var board = Create.Board().With(pizzeriaOrder).Please();

            var order = (await board.GetOrderReadinessToStationary(unitId: 0)).Value as Order;

            Assert.Equal(clientName, order.ClientOrders.First().ClientName);
        }

     
    }
}
