using System.Linq;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Localization;
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

        [Fact]
        public void ReturnDepartmentCultureData_WhichContainedInAvailableCulturesWithTheSameCultureName()
        {
            var departments = new[] {new DepartmentCultureData(1, "111", 1, "0")};
            var departmentStub = new Mock<Department> { CallBase = true };
            departmentStub.SetupGet(x => x.AvailableCultures).Returns(() => new[] { new Cultures("111", "1", true) });
            
            departmentStub.Object.DepartmentCultureData = departments;
            
            Assert.Equal(departments[0].CultureName, departmentStub.Object.DepartmentCultureData[0].CultureName);
        }
        
        [Fact]
        public void ReturnDepartmentCultureDataWithSameDepartmentName_IfNativeCultureNameIsNotNull()
        {
            var departments = new[] {new DepartmentCultureData(1, "333", 1, "0")};
            var departmentStub = new Mock<Department> { CallBase = true };
            departmentStub.SetupGet(x => x.AvailableCultures)
                .Returns(() => new[] { new Cultures("111", "1", true) });
            departmentStub.SetupGet(x => x.Name).Returns("test");

            departmentStub.Object.DepartmentCultureData = departments;
            
            Assert.Equal("test", departmentStub.Object.DepartmentCultureData[0].Name);
        }
        
        [Fact]
        public void ReturnDepartmentCultureDataWithEmptyName_IfNativeCultureNameIsNull()
        {
            var departments = new[] {new DepartmentCultureData(1, "333", 1, "0")};
            var departmentStub = new Mock<Department> { CallBase = true };
            departmentStub.SetupGet(x => x.AvailableCultures)
                .Returns(() => new[] { new Cultures("111", "1", false) });

            departmentStub.Object.DepartmentCultureData = departments;
            
            Assert.Equal("", departmentStub.Object.DepartmentCultureData[0].Name);
        }
    }
}