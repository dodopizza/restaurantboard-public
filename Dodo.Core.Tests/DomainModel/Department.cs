using Dodo.Core.Tests.DomainModel.Dsl;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class Department
    {
        [Fact]
        public void TimeZoneShiftString_ShouldStartWithPlus_WhenTimeZoneShiftIsPositive()
        {
            var department = new DepartmentStub {TempTimeZoneShift = 100 };

            var timeZoneShiftString = department.TimeZoneShiftString;

            Assert.Equal("+100", timeZoneShiftString);
        }
        
        [Fact]
        public void TimeZoneShiftString_ShouldStartWithMinus_WhenTimeZoneShiftIsNegative()
        {
            var department = new DepartmentStub {TempTimeZoneShift = -100 };

            var timeZoneShiftString = department.TimeZoneShiftString;

            Assert.Equal("-100", timeZoneShiftString);
        }
        
        [Fact]
        public void TimeZoneShiftString_ShouldStartWithSpace_WhenTimeZoneShiftIsZero()
        {
            var department = new DepartmentStub {TempTimeZoneShift = 0 };

            var timeZoneShiftString = department.TimeZoneShiftString;

            Assert.Equal(" 0", timeZoneShiftString);
        }
    }
}
