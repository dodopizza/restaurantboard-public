using System;

namespace Dodo.RestaurantBoard.Test.DSL
{
    public static class IntExtensions
    {
        public static DateTime JanOf(this int day, int year)
        {
            return new DateTime(year, 1, day);
        }
    }
}
