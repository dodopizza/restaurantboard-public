using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using System;

namespace Dodo.Tests.DSL
{
    public static class Gimme
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

        public static OrderStorageStubBuilder OrderStorageStub()
        {
            return new OrderStorageStubBuilder();
        }

        public static ProductionOrder ProductionOrder(DateTime changeDate)
        {
            return new ProductionOrder() { ChangeDate = changeDate };
        }
    }
}
