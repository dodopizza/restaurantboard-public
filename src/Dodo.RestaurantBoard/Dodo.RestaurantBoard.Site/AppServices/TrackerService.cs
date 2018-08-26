using System.Threading.Tasks;
using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Site.Core.AppServices;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Site.AppServices
{
    public class TrackerService : ITrackerService
    {
        private readonly ITrackerClient _trackerClient;
        
        public TrackerService(ITrackerClient trackerClient)
        {
            _trackerClient = trackerClient;
        }

        public async Task<ProductionOrder[]> GetOrdersByTypeAsync(
            Uuid unitUuid,
            OrderType type,
            int limit)
        {
            return await _trackerClient.GetOrdersByTypeAsync(unitUuid, type, limit);
        }
    }
}