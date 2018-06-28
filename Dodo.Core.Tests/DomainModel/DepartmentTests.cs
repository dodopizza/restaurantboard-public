using System;
using Dodo.Core.Tests.DomainModel.Dsl;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class DepartmentTests
    {
        [Theory]
        [InlineData(100, "+100")]
        [InlineData(0, " 0")]
        [InlineData(-100, "-100")]
        public void TimeZoneShiftString_StringWithSign(Int16 timeZoneShift, string expected)
        {
            var department = new DepartmentStub {TempTimeZoneShift = timeZoneShift };

            var actual = department.TimeZoneShiftString;

            Assert.Equal(expected, actual);
        }
    }
}
