using Dodo.RestaurantBoard.Domain.Stores;
using Dodo.Tracker.Contracts;

namespace Dodo.RestarauntBoardTests.DslTools
{
    public class OrdersStoreBuilder
    {
        private OrdersStore _ordersStore;

        public OrdersStoreBuilder()
        {
            _ordersStore = new OrdersStore();
        }

        internal OrdersStore Please()
        {
            return _ordersStore;
        }

        internal OrdersStoreBuilder With(IProductionOrder order)
        {
            _ordersStore.AddOrder(order);
            return this;
        }

        internal OrdersStoreBuilder With(ProductionOrderMockBuilder orderMockBuilder)
        {
            _ordersStore.AddOrder(orderMockBuilder.GetMockObject());
            return this;
        }
    }
}