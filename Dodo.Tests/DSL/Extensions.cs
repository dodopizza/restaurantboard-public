using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using System;

namespace Dodo.Tests.DSL
{
    public static class Extensions
    {
        public static ProductionOrder[] GetOrdersWithoutExpiringOnlyParameter(this TrackerClient client)
        {
            return client.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0);
        }

        public static ProductionOrder[] GetOrdersWithExpiringOnlyParameterEqualToTrue(this TrackerClient client)
        {
            return client.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0, true);
        }

        public static DateTime July(this int day, int year)
        {
            return  new DateTime(year, 7, day);
        }

        public static DateTime TimeIs(this DateTime dateTime, string time)
        {
            var timeParts = time.Split(":");

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, int.Parse(timeParts[0]), int.Parse(timeParts[1]), 0);
        }
    }
}
