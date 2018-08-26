using System.Threading.Tasks;
using Dodo.Core.Common;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;

namespace Dodo.RestaurantBoard.Site.Core.AppServices
{
    public interface ITrackerService
    {
        Task<ProductionOrder[]> GetOrdersByTypeAsync(
            Uuid unitUuid,
            OrderType type,
            int limit);
    }
}