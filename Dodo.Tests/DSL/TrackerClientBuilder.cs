using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Moq;
using System;

namespace Dodo.Tests.DSL
{
    public class TrackerClientBuilder
    {
        private IDateProvider _dateProvider;

        private IOrdersProvider _ordersProvider;

        internal TrackerClientBuilder WithDateProviderAs(Mock<IDateProvider> dateProviderMock)
        {
            _dateProvider = dateProviderMock.Object;

            return this;
        }

        internal TrackerClientBuilder WithOrdersProviderAs(Mock<IOrdersProvider> ordersProviderMock)
        {
            _ordersProvider = ordersProviderMock.Object;

            return this;
        }

        internal TrackerClientBuilder WithOrders(params ProductionOrder[] orders)
        {
            _ordersProvider = Create.OrdersProvider.WithOrders(orders).Please();

            return this;
        }

        internal TrackerClientBuilder WithNowAs(DateTime nowDate)
        {
            _dateProvider = Create.DateProvider.WithNowAs(nowDate).Please().Object;

            return this;
        }

        internal TrackerClient Please()
        {
            return new TrackerClient(_ordersProvider ?? new Mock<IOrdersProvider>().Object, _dateProvider ?? new DateProvider());
        }
    }
}
