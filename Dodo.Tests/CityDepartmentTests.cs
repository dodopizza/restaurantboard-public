using Dodo.Core.DomainModel.Departments.Departments;
using Xunit;

namespace Dodo.Tests
{
    public class CityDepartmentTests
    {
        [Fact]
        public void ShouldReturnZeroUtcOffset()
        {
            var cityDepartment = new CityDepartment(new TestUtcOffsetProvider(2));

            Assert.Equal("-00:00", cityDepartment.TimeZoneUTCOffsetString);
        }
        
        [Fact]
        public void ShouldReturnTimeZoneShiftStringEqualMinusTwo()
        {
            var cityDepartment = new CityDepartment(new TestUtcOffsetProvider(2));

            Assert.Equal("-2", cityDepartment.TimeZoneShiftString);
        }
    }
}
