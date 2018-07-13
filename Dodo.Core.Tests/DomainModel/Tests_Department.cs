using Dodo.Core.DomainModel.Departments;
using Moq;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class Tests_Department
    {
        [Fact]
        public void TimeZoneShiftString_ShouldStartWithPlus_WhenTimeZoneShiftIsPositive()
        {
            var department = new Department() {ManagedTimeZoneShift = 100};

            var timeZoneShiftString = department.ManagedTimeZoneShiftString;

            Assert.Equal("+100", timeZoneShiftString);
        }

        [Fact]
        public void TimeZoneShiftString_ShouldStartWithMinus_WhenTimeZoneShiftIsNegative()
        {
            var department = new Department {ManagedTimeZoneShift = -100};

            var timeZoneShiftString = department.ManagedTimeZoneShiftString;

            Assert.Equal("-100", timeZoneShiftString);
        }

        [Fact]
        public void TimeZoneShiftString_ShouldStartWithSpace_WhenTimeZoneShiftIsZero()
        {
            var department = new Department {ManagedTimeZoneShift = 0};

            var timeZoneShiftString = department.ManagedTimeZoneShiftString;

            Assert.Equal(" 0", timeZoneShiftString);
        }

        #region StateTests
        
        [Fact]
        public void ShouldContainsOneUnit_WhenAddUnit()
        {
            var unit = new Unit();
            var department = new Department();
            
            department.AddUnit(unit);
            
            Assert.Equal(1, department.Units.Count);
        }
        
        [Fact]
        public void ShouldContainsTwoUnits_WhenAddTwoUnits()
        {
            var unitOne = new Unit();
            var unitTwo = new Unit();
            var department = new Department();
            
            department.AddUnit(unitOne);
            department.AddUnit(unitTwo);
            
            Assert.Equal(2, department.Units.Count);
        }
        
        [Fact]
        public void WithTwoUnits_ShouldContainsTwoUnitNames_WhenGetAllUnitNames()
        {
            var unitOne = new Unit();
            var unitTwo = new Unit();
            var department = new Department();
            department.AddUnit(unitOne);
            department.AddUnit(unitTwo);

            var unitNames = department.GetAllUnitNames();
            
            Assert.Equal(2, unitNames.Count);
        }

        #endregion
    }
}