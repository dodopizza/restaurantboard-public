using Dodo.RestaurantBoard.Domain.Services;
using Moq;

namespace Dodo.Tests.DSL
{
    public class TrackerClientBuilder
    {
        private IDateProvider _dateProvider;

        private IOrdersProvider _ordersProvider;

        internal TrackerClientBuilder WithDateProviderAs(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;

            return this;
        }

        internal TrackerClientBuilder WithDateProviderAs(Mock<IDateProvider> dateProviderMock)
        {
            _dateProvider = dateProviderMock.Object;

            return this;
        }

        internal TrackerClientBuilder WithOrdersProviderAs(IOrdersProvider ordersProvider)
        {
            _ordersProvider = ordersProvider;

            return this;
        }

        internal TrackerClientBuilder WithOrdersProviderAs(Mock<IOrdersProvider> ordersProviderMock)
        {
            _ordersProvider = ordersProviderMock.Object;

            return this;
        }

        internal TrackerClient Please()
        {
            return new TrackerClient(_ordersProvider ?? new Mock<IOrdersProvider>().Object, _dateProvider ?? new DateProvider());
        }
    }
}
