using System.Threading.Tasks;
using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Test
{
    public class TrackerClientStub : ITrackerClient
    {
        private readonly ProductionOrder[] _orders;

        public TrackerClientStub(ProductionOrder[] orders)
        {
            _orders = orders;
        }

        public Task<ProductionOrder[]> GetOrdersByTypeAsync(Uuid unitUuid, OrderType type, int limit)
        {
            return Task.FromResult(_orders);
        }
    }
}
