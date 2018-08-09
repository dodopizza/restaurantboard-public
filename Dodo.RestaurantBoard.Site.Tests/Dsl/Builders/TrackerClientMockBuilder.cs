using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Builders
{
    public class TrackerClientMockBuilder
    {
        private Mock<ITrackerClient> _service;

        public TrackerClientMockBuilder()
        {
            _service = new Mock<ITrackerClient>();
        }

        public TrackerClientMockBuilder WithEmptyOrderList()
        {
            _service
                .Setup(x => x.GetOrdersByType(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<OrderState[]>(), It.IsAny<int>()))
                .Returns(() => new ProductionOrder[0]);
            return this;
        }

        public ITrackerClient Please()
        {
            return _service.Object;
        }

        public TrackerClientMockBuilder WithOneOrder(ProductionOrder productionOrder)
        {
            _service
                .Setup(x => x.GetOrdersByType(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<OrderState[]>(), It.IsAny<int>()))
                .Returns(() => new [] { productionOrder });
            return this;
        }
    }
}