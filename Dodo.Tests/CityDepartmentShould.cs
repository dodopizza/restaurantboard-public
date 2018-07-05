using System;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Xunit;

namespace Dodo.Tests
{
    public class CityDepartmentShould
    {
       [Fact]
        public void SetZeroTimeShiftForUtcPlusThreeTimezone()
        {
            Department department = new CityDepartment
            {
                TimeZoneUTCOffset = (int)TimeSpan.FromHours(3).TotalMinutes
            };

            var actualTimeZoneShift = department.TimeZoneShift;

            Assert.Equal(0, actualTimeZoneShift);
        }

        [Fact]
        public void SetMinus3HoursTimeShiftForZeroUtcTimezone()
        {
            Department department = new CityDepartment
            {
                TimeZoneUTCOffset = 0
            };

            var actualTimeZoneShift = department.TimeZoneShift;

            Assert.Equal(-3, actualTimeZoneShift);
        }
    }
}
