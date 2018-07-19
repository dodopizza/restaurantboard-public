using System;

namespace Dodo.RestarauntBoardTests.DslTools
{
    public static class IntExtensions
    {
        public static DateTime January(this int day, int year)
        {
            return new DateTime(year, 1, day);
        }
    }
}