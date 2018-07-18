using Dodo.Tracker.Contracts;

namespace Dodo.Tests.DSL
{
    public static class Create
    {
        public static DateProviderMockBuilder DateProvider => new DateProviderMockBuilder();

        public static OrdersProviderMockBuilder OrdersProvider => new OrdersProviderMockBuilder();

        public static TrackerClientBuilder TrackerClient => new TrackerClientBuilder();

        public static ProductionOrderBuilder Order => new ProductionOrderBuilder();

        public static ProductionOrder[] Orders(int count)
        {
            return new ProductionOrder[count];
        }
    }
}
