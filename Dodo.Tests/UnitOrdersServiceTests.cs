using System;
using System.Linq;
using System.Threading.Tasks;
using Dodo.Core.AppServices;
using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using NUnit.Framework;
using Tests.DSL;

namespace Tests
{
    public class UnitOrdersServiceTests
    {
        [Test]
        public async Task ShouldReturnOrders_ForUnitId()
        {
            var trackerClientStub = Create.TrackerClient
                .WithFakeOrders(new ProductionOrder { ClientName = "Лупа" })
                .Build();
            var departmentStructureStub = Create.DepartmentsStructureService
                .WithPizzeria(id: 29)
                .Build();
            var unitOrdersService = new UnitOrdersService(trackerClientStub, departmentStructureStub);

            var unitOrders = await unitOrdersService.GetUnitOrders(29);

            Assert.AreEqual(29, unitOrders.Unit.Id);
            Assert.AreEqual("Лупа", unitOrders.Orders.Single().ClientName);
        }
    }
}