using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Moq;
using System.Collections.Generic;

namespace Dodo.Tests.DSL
{
    public class OrdersProviderMockBuilder
    {
        private readonly Mock<IOrdersProvider> _provider;
        private readonly List<ProductionOrder> _orders;

        public OrdersProviderMockBuilder()
        {
            _provider = new Mock<IOrdersProvider>();
            _orders = new List<ProductionOrder>();
        }

        internal OrdersProviderMockBuilder AddOrder(ProductionOrder order)
        {
            _orders.Add(order);

            return this;
        }

        internal OrdersProviderMockBuilder AddOrders(ProductionOrder[] orders)
        {
            _orders.AddRange(orders);

            return this;
        }
       
        internal IOrdersProvider Please()
        {
            _provider.Setup(_ => _.GetOrders()).Returns(_orders.ToArray());

            return _provider.Object;
        }

        public static implicit operator Mock<IOrdersProvider>(OrdersProviderMockBuilder builder)
        {
            return builder._provider;
        }
    }
}
