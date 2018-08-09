using Dodo.Core.DomainModel.Departments;
using Moq;
using Xunit;

namespace Dodo.Core.Tests
{
    public class DepartmentShould
    {
        [Fact]
        public void ReturnPlus2String_WhenTimeZoneShiftIs2()
        {
            var departmentStub = new Mock<Department> { CallBase = true };
            departmentStub.SetupGet(x => x.TimeZoneShift).Returns(2);

            var timeZoneShiftString = departmentStub.Object.TimeZoneShiftString;

            Assert.Equal("+2", timeZoneShiftString);
        }
    }
}