using Dodo.Core.DomainModel.Departments;
using Dodo.Core.Tests.DomainModel.DSL;
using Moq;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class Tests_Department
    {
        [Fact]
        public void TimeZoneShiftString_ShouldStartWithPlus_WhenTimeZoneShiftIsPositive()
        {
            var department = Create.Department.WithTimeZoneShift(100).Please();
            
            var timeZoneShiftString = department.ManagedTimeZoneShiftString;

            Assert.Equal("+100", timeZoneShiftString);
        }

        [Fact]
        public void TimeZoneShiftString_ShouldStartWithMinus_WhenTimeZoneShiftIsNegative()
        {
            var department = Create.Department.WithTimeZoneShift(-100).Please();

            var timeZoneShiftString = department.ManagedTimeZoneShiftString;

            Assert.Equal("-100", timeZoneShiftString);
        }

        [Fact]
        public void TimeZoneShiftString_ShouldStartWithSpace_WhenTimeZoneShiftIsZero()
        {
            var department = Create.Department.WithTimeZoneShift(0).Please();

            var timeZoneShiftString = department.ManagedTimeZoneShiftString;

            Assert.Equal(" 0", timeZoneShiftString);
        }

        #region StateTests
        
        [Fact]
        public void ShouldContainsOneUnit_WhenAddUnit()
        {
            var department = Create.Department.WithUnit(Create.Unit.Please()).Please();
            
            Assert.Equal(1, department.Units.Count);
        }
        
        [Fact]
        public void ShouldContainsTwoUnits_WhenAddTwoUnits()
        {
            var department = Create.Department.WithUnits(2).Please();
            
            Assert.Equal(2, department.Units.Count);
        }
        
        [Fact]
        public void WithTwoUnits_ShouldContainsTwoUnitNames_WhenGetAllUnitNames()
        {
            var department = Create.Department.WithUnits(2).Please();

            var unitNames = department.GetAllUnitNames();
            
            Assert.Equal(2, unitNames.Count);
        }

        #endregion
    }
}