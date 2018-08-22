using Dodo.RestaurantBoard.Domain.Services;
using Xunit;

namespace Dodo.RestaurantBoard.Test
{
    public class PizzeriaOrdersServiceTests
    {
        [Fact]
        public void WhenGetOrdersByUnitIdAsync_ShouldReturnCorrectOrders()
        {
            var service = new PizzeriaOrdersService(
                new DepartmentsStructureService(),
                new TrackerClientStub());

            var pizzeriaOrders = service.GetOrdersByUnitIdAsync(10).Result;

            Assert.Equal(29, pizzeriaOrders.Pizzeria.Id);
            Assert.Equal(2, pizzeriaOrders.Orders.Length);
        }
    }
}