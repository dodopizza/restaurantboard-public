using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Tests.DSL;

namespace Dodo.Tests
{
    public class BoardsControllerTests
    {
        [Fact]
        public async Task ShouldReturnOrder_WithLupaName()
        {
            var pizzeriaOrderServirce = new PizzeriaOrdersServiceStub();

            var boardsController = new BoardsController(clientsService: new ClientService(),
                managementService: null,
                hostingEnvironment: null,
                pizzeriaOrdersService: pizzeriaOrderServirce);
            var order = (await boardsController.GetOrderReadinessToStationary(unitId: 0)).Value as Order;

            Assert.Equal("Лупа", order.ClientOrders.First().ClientName);
        }

        public class PizzeriaOrdersServiceStub : IPizzeriaOrdersService
        {
            public Department GetDepartmentByUnitOrCache(int unitId)
            {
                return new CityDepartment();
            }

            public T GetDepartmentOrCache<T>(int departmentId) where T : Department
            {
                return new CityDepartment() as T;
            }

            public Task<ProductionOrder[]> GetOrdersByTypeAsync(Uuid unitUuid, OrderType type, int limit)
            {
                var orders = new[]
              {
                new ProductionOrder
                {
                    Id = 56,
                    Number = 4,
                    ClientName = "Лупа",
                    ChangeDate = DateTime.Now.AddMinutes(-3)
                }
            };
                return Task.FromResult(orders);
            }

            public Pizzeria GetPizzeriaOrCache(int id)
            {
                return PizzeriaStub.GetPizzeria();
            }


            public Unit GetUnitOrCache(Uuid unitUuid)
            {
                return PizzeriaStub.GetPizzeria();
            }
        }
    }
}
