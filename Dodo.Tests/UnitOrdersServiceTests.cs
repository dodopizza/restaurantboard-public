using System;
using System.Threading.Tasks;
using Dodo.Core.AppServices;
using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using NUnit.Framework;

namespace Tests
{
    public class UnitOrdersServiceTests
    {
        [Test]
        public async Task ShouldReturnOrders_ForUnitId()
        {
            var unitOrdersService = new UnitOrdersService(
                new TrackerClientStub(),
                new DepartmentsStructureService());

            var unitOrders = await unitOrdersService.GetUnitOrders(29);

            Assert.AreEqual("Лупа", unitOrders.Orders[0].ClientName);
            Assert.AreEqual(29, unitOrders.Unit.Id);
        }
    }

    public class TrackerClientStub : ITrackerClient
    {
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
    }
}