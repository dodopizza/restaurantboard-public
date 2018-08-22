using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;

namespace Dodo.RestaurantBoard.Tests.DSL
{
    public class PizzeriaOrdersServiceBuilder
    {
        private ITrackerClient _trackerClient;

        public PizzeriaOrdersServiceBuilder With(ITrackerClient trackerClient)
        {
            _trackerClient = trackerClient;
            return this;
        }

        public IPizzeriaOrdersService Please()
        {
            return new PizzeriaOrdersService(_trackerClient);
        }
    }
}
