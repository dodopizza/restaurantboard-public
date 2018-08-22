using Dodo.Core.Services;
using Dodo.Tracker.Contracts;

namespace Tests.DSL
{
    public class TrackerClientBuilder
    {
        private ProductionOrder[] _fakeOrders;

        public TrackerClientBuilder WithFakeOrders(params ProductionOrder[] fakeOrders)
        {
            _fakeOrders = fakeOrders;
            return this;
        }

        public ITrackerClient Build()
        {
            return new TrackerClientFake(_fakeOrders);
        }
    }
}