using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Xunit;

namespace Dodo.Tests
{
    public class CityDepartmentTests
    {
        [Theory]
        [InlineData(180, 0)]
        [InlineData(150, 0)]
        [InlineData(120, -1)]
        [InlineData(0, -3)]
        public void CheckTimeZoneShift(int timeZoneUtcOffset, int expectedTimeZoneShift)
        {
            Department testDepartment = new CityDepartment
            {
                TimeZoneUTCOffset = timeZoneUtcOffset
            };

            var actualTimeZoneShift = testDepartment.TimeZoneShift;

            Assert.Equal(expectedTimeZoneShift, actualTimeZoneShift);
        }
    }
}
