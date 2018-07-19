using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;

namespace Dodo.Tests.DSL
{
    public class TrackerClientBuilder
    {
        private IOrdersStorage _ordersStorage;

        public TrackerClientBuilder()
        {
            SetDefault();
        }

        public TrackerClientBuilder Default()
        {
            SetDefault();
            return this;
        }

        public TrackerClientBuilder WithOrderStorage(IOrdersStorage ordersStorage)
        {
            _ordersStorage = ordersStorage;
            return this;
        }

        public TrackerClientBuilder WithExistingOrder(string clientName, int number)
        {
            _ordersStorage.AddProductionOrder(clientName, number);
            return this;
        }

        public ITrackerClient RightNow()
        {
            return new TrackerClient(_ordersStorage);
        }

        private void SetDefault()
        {
            _ordersStorage = Gimmy.OrderStorage().Default().RightNow();
        }
    }
}
