using System.Collections.Generic;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;

namespace Dodo.RestaurantBoard.Test.DSL
{
    public class TrackerClientBuilder
    {
        private readonly List<ProductionOrder> _orders = new List<ProductionOrder>();

        public TrackerClientBuilder With(ProductionOrder order)
        {
            _orders.Add(order);
            return this;
        }

        public ITrackerClient Please()
        {
            return new TrackerClientStub(_orders.ToArray());
        }
    }
}