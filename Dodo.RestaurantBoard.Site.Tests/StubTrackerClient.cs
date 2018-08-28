using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using System;
using System.Threading.Tasks;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class StubTrackerClient : ITrackerClient
    {
        public Task<ProductionOrder[]> GetOrdersByTypeAsync(Uuid unitUuid, OrderType type, int limit)
        {
            throw new NotImplementedException();
        }
    }
}
