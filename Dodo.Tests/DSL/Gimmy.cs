using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using System;

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

        public static DateTimeProviderStub DateTimeProviderStub()
        {
            return new DateTimeProviderStub();
        }

        public static DateTimeProviderStub DateTimeProviderStub(DateTime dateTime)
        {
            return new DateTimeProviderStub(dateTime);
        }
    }
}
