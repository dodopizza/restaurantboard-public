using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Moq;
using System.Threading.Tasks;

namespace Dodo.RestaurantBoard.Tests.DSL
{
    public class TrackerBuilder
    {
        private Mock<ITrackerClient> _trackerClient;

        public TrackerBuilder()
        {
            _trackerClient = new Mock<ITrackerClient>();
        }

        public TrackerBuilder WithOrderFrom(string name)
        {
            _trackerClient
                .Setup(x => x.GetOrdersByTypeAsync(It.IsAny<Uuid>(), It.IsAny<Tracker.Contracts.Enums.OrderType>(), It.IsAny<int>()))
                .Returns(Task.FromResult(new[] { new ProductionOrder() { ClientName = name } }));

            return this;
        }

        public ITrackerClient Please()
        {
            return _trackerClient.Object;
        }
    }
}
