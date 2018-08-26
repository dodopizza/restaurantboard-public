namespace Dodo.RestaurantBoard.Test.DSL
{
    public static class Create
    {
        public static PizzeriaOrdersServiceBuilder PizzeriaOrdersService => new PizzeriaOrdersServiceBuilder();
        public static TrackerClientBuilder TrackerClient => new TrackerClientBuilder();
        public static BoardsControllerBuilder BoardsController => new BoardsControllerBuilder();
        public static OrderBuilder ProductionOrder => new OrderBuilder();
    }
}