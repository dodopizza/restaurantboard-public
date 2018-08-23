using Dodo.Core.Services;
using Dodo.Tracker.Contracts;

namespace Dodo.RestaurantBoard.Test.DSL
{
    public class TrackerClientBuilder
    {
        private ProductionOrder[] _orders;
        
        public TrackerClientBuilder WithEmptyOrders(int count)
        {
            _orders = new ProductionOrder[2];
            for (var i = 0; i < count; i++)
            {
                _orders[i] = new ProductionOrder();
            }
            return this;
        }

        public ITrackerClient Please()
        {
            return new TrackerClientStub(_orders);
        }
    }
}