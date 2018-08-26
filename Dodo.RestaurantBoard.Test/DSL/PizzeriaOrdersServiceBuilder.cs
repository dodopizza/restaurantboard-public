using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;

namespace Dodo.RestaurantBoard.Test.DSL
{
    public class PizzeriaOrdersServiceBuilder
    {
        private ITrackerClient _trackerClient;

        public PizzeriaOrdersServiceBuilder WithTrackerClient(ITrackerClient trackerClient)
        {
            _trackerClient = trackerClient;
            return this;
        }
        
        public PizzeriaOrdersService Please()
        {
            return new PizzeriaOrdersService(
                new DepartmentsStructureService(),
                _trackerClient ?? new TrackerClientStub(null));
        }
    }
}