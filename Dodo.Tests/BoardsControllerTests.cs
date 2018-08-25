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
        public async Task ShouldReturnOrderFromGetOrderReadinessToStationary_AreEqualWithOrderFromPizzeriaOrders()
        {
            var pizzeriaOrder = Create.PizzeriaOrder().AddClientWithName("Лупа").Please();
            var board = Create.BoardController().With(pizzeriaOrder).Please();

            var order = (await board.GetOrderReadinessToStationary(unitId: 0)).Value as Order;

            Assert.Equal("Лупа", order.ClientOrders.Single().ClientName);
        }


    }
}
