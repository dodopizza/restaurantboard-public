using System;
using System.Threading.Tasks;
using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Tests
{
    public class TrackerClientStub : ITrackerClient
    {
        public Task<ProductionOrder[]> GetOrdersByTypeAsync(Uuid unitUuid, OrderType type, int limit)
        {
            return Task.FromResult(new ProductionOrder[]
            {
                new ProductionOrder(),
                new ProductionOrder(),
            });
        }
    }
}
