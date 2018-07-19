using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;

namespace Dodo.Tests.DSL
{
    public static class Gimmy
    {
        public static TrackerClientBuilder TrackerClient()
        {
            return new TrackerClientBuilder();
        }

        public static OrderStorageBuilder OrderStorage()
        {
            return new OrderStorageBuilder();
        }

        public static IDateTimeProvider DefaultDateTimeProvider() => new DateTimeProviderUtcNow();

        //public static DateTimeProviderBuilder DateTimeProvider()
        //{
        //    return new DateTimeProviderBuilder();
        //}
    }
}
