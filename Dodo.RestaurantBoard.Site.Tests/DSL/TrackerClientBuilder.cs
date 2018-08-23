using System.Threading.Tasks;
using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    public class TrackerClientBuilder
    {
        private readonly Mock<ITrackerClient> _service;

        public TrackerClientBuilder()
        {
            _service = new Mock<ITrackerClient>();
        }

        public TrackerClientBuilder WithOrders(ProductionOrder[] orders)
        {
            _service
                .Setup(x => x.GetOrdersByTypeAsync(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<int>()))
                .Returns(Task.FromResult(orders));

            return this;
        }

        public ITrackerClient Please()
        {
            return _service.Object;
        }
    }
}