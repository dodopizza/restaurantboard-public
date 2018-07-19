using Dodo.RestaurantBoard.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.Tests.DSL
{
    public static class Extensions
    {
        public static int NumberFor(this ITrackerClient trackerClient, string name)
        {
            return trackerClient.GetOrderByName(name).Number;
        }

        public static DateTime ChangeDateFor(this ITrackerClient trackerClient, string name)
        {
            return trackerClient.GetOrderByName(name).ChangeDate.Value;
        }

        public static void ShouldBe(this int result, int expected)
        {
            Assert.Equal(expected, result);
        }

        public static void ShouldBe(this DateTime result, DateTime expected)
        {
            Assert.Equal(expected, result);
        }

        public static DateTime January(this int day, int year)
        {
            if (day <= 0 || day > 31)
                throw new ArgumentException($"{nameof(day)} should be between 1 and 31!");

            return new DateTime(year, 1, day);
        }

        public static DateTime June(this int day, int year)
        {
            if (day <= 0 || day > 30)
                throw new ArgumentException($"{nameof(day)} should be between 1 and 30!");

            return new DateTime(year, 6, day);
        }

        public static DateTime December(this int day, int year)
        {
            if (day <= 0 || day > 31)
                throw new ArgumentException($"{nameof(day)} should be between 1 and 31!");

            return new DateTime(year, 12, day);
        }
        // and so on ...
        // ...

        public static TimeSpan Seconds (this int seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }
        // and so on ...
        // ...
        public static TimeSpan Days(this int days)
        {
            return TimeSpan.FromDays(days);
        }
    }

    public static class Difference
    {
        public static DifferenceBuilder Between(DateTime dateTime)
        {
            return new DifferenceBuilder(dateTime);
        }
    }

}
