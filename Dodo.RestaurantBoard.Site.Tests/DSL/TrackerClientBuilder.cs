using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    internal class TrackerClientBuilder
    {
        private ProductionOrder[] _productionOrders;

        public TrackerClientBuilder WithProductionOrders(ProductionOrder[] productionOrders)
        {
            _productionOrders = productionOrders;
            return this;
        }

        public ITrackerClient Please()
        {
            var trackerClientMock = new Mock<ITrackerClient>();
            trackerClientMock
                .Setup(tc => tc.GetOrdersByTypeAsync(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<int>()))
                .ReturnsAsync(_productionOrders);

            return trackerClientMock.Object;
        }
    }
}
