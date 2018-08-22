using System;
using System.Linq;
using System.Threading.Tasks;
using Dodo.Core.DomainModel.OrderProcessing;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.Tracker.Contracts;
using NUnit.Framework;
using Tests.DSL;

namespace Tests
{
    public class BoardsControllerTests
    {
        [Test]
        public async Task GetOrderReadinessToStationary_ShouldReturnCorrectClientOrder_ForUnitId()
        {
            var clientServiceStub = Create.ClientService
                .WithoutIcons()
                .Build();
            var unitOrderServiceStub = Create.UnitOrderService
                .WithPizzeria(unitId: 29)
                .WithOrders(CreateOrderWithClientName("Пупа"))
                .Build();
            var boardsController = new BoardsController(
                clientsService: clientServiceStub,
                unitOrdersService: unitOrderServiceStub,
                departmentsStructureService: null,
                managementService: null,
                hostingEnvironment: null
            );

            var order = (await boardsController.GetOrderReadinessToStationary(unitId: 29)).Value as IOrder;

            Assert.AreEqual("Пупа", order.ClientOrders.Single().ClientName);
        }

        private static RestaurantReadnessOrders CreateOrderWithClientName(string clientName)
        {
            return new RestaurantReadnessOrders(
                orderId: 1,
                orderNumber: 1,
                clientName: clientName,
                orderReadyDateTime: DateTime.Now
            );
        }
    }
}