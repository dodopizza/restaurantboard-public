using System;

namespace Tests.Dsl
{
    public static class NumberExtensions
    {
        public static DateTime July(this int day, int year)
        {
            return new DateTime(year, 7, day);
        }
    }
}