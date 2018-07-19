namespace Dodo.RestarauntBoardTests.DslTools
{
    public static class Create
    {
        public static OrdersStoreBuilder OrderStore => new OrdersStoreBuilder();
        public static ProductionOrderMockBuilder OrderToWatch => new ProductionOrderMockBuilder();
        public static ProductionOrderBuilder Order => new ProductionOrderBuilder();
    }
}