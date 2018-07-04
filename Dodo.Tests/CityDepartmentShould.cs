using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Xunit;

namespace Dodo.Tests
{
    public class CityDepartmentShould
    {
        [Fact]
        public void SetZeroTimeShiftForMoscowTimezone()
        {
            Department testDepartment = new CityDepartment
            {
                TimeZoneUTCOffset = 180
            };

            var actualTimeZoneShift = testDepartment.TimeZoneShift;

            Assert.Equal(0, actualTimeZoneShift);
        }

        [Fact]
        public void SetMinus3HoursTimeShiftForZeroUtcTimezone()
        {
            Department testDepartment = new CityDepartment
            {
                TimeZoneUTCOffset = 0
            };

            var actualTimeZoneShift = testDepartment.TimeZoneShift;

            Assert.Equal(-3, actualTimeZoneShift);
        }
    }
}
