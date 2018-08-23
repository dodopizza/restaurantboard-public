namespace Dodo.RestaurantBoard.Test.DSL
{
    public static class Create
    {
        public static PizzeriaOrdersServiceBuilder PizzeriaOrdersServiceBuilder => new PizzeriaOrdersServiceBuilder();
        public static TrackerClientBuilder TrackerClientBuilder => new TrackerClientBuilder();
        public static BoardsControllerBuilder BoardsControllerBuilder => new BoardsControllerBuilder();
    }
}