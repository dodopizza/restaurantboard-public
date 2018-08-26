using Dodo.Core.Services;
using Dodo.RestaurantBoard.Site.AppServices;
using Dodo.RestaurantBoard.Site.Core.AppServices;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    internal class TrackerServiceBuilder
    {
        private ITrackerClient _trackerClient;

        public TrackerServiceBuilder WithTrackerClient(ITrackerClient trackerClient)
        {
            _trackerClient = trackerClient;
            return this;
        }

        public ITrackerService Please()
        {
            var trackerService = new TrackerService(_trackerClient);

            return trackerService;
        }
    }
}
