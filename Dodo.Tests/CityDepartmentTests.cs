using Dodo.Core.DomainModel.Departments.Departments;
using System;
using Xunit;

namespace Dodo.Tests
{
    public class CityDepartmentTests
    {
        [Fact]
        public void ShouldReturnZeroUtcOffset()
        {
            var cityDepartment = new CityDepartment();

            Assert.Equal("-00:00", cityDepartment.TimeZoneUTCOffsetString);
        }
        
        
        [Fact]
        public void TimeZoneShiftString()
        {
            var cityDepartment = new CityDepartment();

            Assert.Equal("-1", cityDepartment.TimeZoneShiftString);
        }
    }
}
