using System.Linq;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Site.Tests.DSL;
using Dodo.RestaurantBoard.Site.ViewModel;
using Dodo.Tracker.Contracts;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class BoardsControllerTests
    {
        [Fact]
        public async void GetOrderReadinessToStationary_ShouldReturnOrderForJohnDoe()
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
                .WithOrders(new [] { new ProductionOrder { Number = 42, ClientName = "John Doe" } })
                .Please();
            var boardsController = new BoardsController(departmentStructureServiceStub, clientsServiceStub, null, trackerClientStub, null);

            var board = (await boardsController.GetOrderReadinessToStationary(unitId: 1)).Value as Board;

            Assert.Equal(42, board.ClientOrders.Single().OrderNumber);
            Assert.Equal("John Doe", board.ClientOrders.Single().ClientName);
        }
    }
}
