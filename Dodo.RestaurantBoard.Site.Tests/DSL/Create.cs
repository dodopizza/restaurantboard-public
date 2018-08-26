
namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    internal class Create
    {
        public static BoardControllerBuilder BoardController => new BoardControllerBuilder();
        public static TrackerServiceBuilder TrackerService => new TrackerServiceBuilder();
        public static TrackerClientBuilder TrackerClient => new TrackerClientBuilder();
    }
}
