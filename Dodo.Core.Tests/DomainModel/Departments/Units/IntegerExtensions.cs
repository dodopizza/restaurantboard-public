using System;

namespace Dodo.Core.Tests.DomainModel.Departments
{
    public static class IntegerExtensions
    {
        public static DateTime JulyOf(this int day, int year)
        {
            return new DateTime(year, 7, day);
        }
        
        public static DateTime JuneOf(this int day, int year)
        {
            return new DateTime(year, 6, day);
        }
    }
}