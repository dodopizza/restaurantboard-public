using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
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
        
        [Fact]
        public void WhenTest()
        {
            var service = new PizzeriaOrdersService(
                new DepartmentsStructureService(),
                new TrackerClientStub());
            var controller = new BoardsController(new DepartmentsStructureService(), new ClientService(), new ManagementService(), null, service);

            var result = controller.GetOrderReadinessToStationary(10);

            
        }
    }
}