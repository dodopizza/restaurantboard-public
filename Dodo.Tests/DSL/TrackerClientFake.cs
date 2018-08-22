using System;
using System.Threading.Tasks;
using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Tests.DSL
{
    public class TrackerClientFake : ITrackerClient
    {
        private readonly ProductionOrder[] _fakeOrders;

        public TrackerClientFake(ProductionOrder[] fakeOrders)
        {
            _fakeOrders = fakeOrders;
        }

        public Task<ProductionOrder[]> GetOrdersByTypeAsync(Uuid unitUuid, OrderType type, int limit)
        {
            return Task.FromResult(_fakeOrders);
        }
    }
}