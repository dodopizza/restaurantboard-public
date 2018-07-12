using System;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.Tests.DomainModel.Dsl;
using Moq;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class Department
    {
        [Fact]
        public void TimeZoneShiftString_ShouldStartWithPlus_WhenTimeZoneShiftIsPositive()
        {
            var department = new DepartmentFake {TempTimeZoneShift = 100 };

            var timeZoneShiftString = department.TimeZoneShiftString;

            Assert.Equal("+100", timeZoneShiftString);
        }
        
        [Fact]
        public void TimeZoneShiftString_ShouldStartWithMinus_WhenTimeZoneShiftIsNegative()
        {
            var department = new DepartmentFake {TempTimeZoneShift = -100 };

            var timeZoneShiftString = department.TimeZoneShiftString;

            Assert.Equal("-100", timeZoneShiftString);
        }
        
        [Fact]
        public void TimeZoneShiftString_ShouldStartWithSpace_WhenTimeZoneShiftIsZero()
        {
            var department = new DepartmentFake {TempTimeZoneShift = 0 };

            var timeZoneShiftString = department.TimeZoneShiftString;

            Assert.Equal(" 0", timeZoneShiftString);
        }

        #region StateTests

        [Fact]
        public void ShouldCallToStringOffice_WhenUnitTypeIsOfficeStub()
        {
            var unitStub = UnitStub.Create(UnitType.Office);
            var department = new DepartmentFake();
            department.AddUnit(unitStub);

            department.GetAllUnitsNames();

            Assert.Equal(1, unitStub.ToStringOfficeCounter);
        }
        
        [Fact]
        public void ShouldCallToStringPizzeria_WhenUnitTypeIsPizzeriaStub()
        {
            var unitStub = UnitStub.Create(UnitType.Pizzeria);
            var department = new DepartmentFake();
            department.AddUnit(unitStub);

            department.GetAllUnitsNames();

            Assert.Equal(1, unitStub.ToStringPizzeriaCounter);
        }
        
        [Fact]
        public void ShouldCallToStringCallCenter_WhenUnitTypeIsCallCenterStub()
        {
            var unitStub = UnitStub.Create(UnitType.CallCenter);
            var department = new DepartmentFake();
            department.AddUnit(unitStub);

            department.GetAllUnitsNames();

            Assert.Equal(1, unitStub.ToStringCallCenterCounter);
        }
        
        [Fact]
        public void ShouldCallToStringWarehouse_WhenUnitTypeIsWarehouseStub()
        {
            var unitStub = UnitStub.Create(UnitType.Warehouse);
            var department = new DepartmentFake();
            department.AddUnit(unitStub);

            department.GetAllUnitsNames();

            Assert.Equal(1, unitStub.ToStringWarehouseCounter);
        }
        
        [Fact]
        public void ShouldCallToStringServiceDelivery_WhenUnitTypeIsServiceDeliveryStub()
        {
            var unitStub = UnitStub.Create(UnitType.ServiceDelivery);
            var department = new DepartmentFake();
            department.AddUnit(unitStub);

            department.GetAllUnitsNames();

            Assert.Equal(1, unitStub.ToStringServiceDeliveryCounter);
        } 
        
        [Fact]
        public void ShouldCallToString_WhenUnitTypeIsProductionDistributionWorkshopStub()
        {
            var unitStub = UnitStub.Create(UnitType.ProductionDistributionWorkshop);
            var department = new DepartmentFake();
            department.AddUnit(unitStub);

            department.GetAllUnitsNames();

            Assert.Equal(1, unitStub.ToStringDefaultCounter);
        }
        
        [Fact]
        public void ShouldCallSpecificToString_WhenUnitTypeIsOfficeAndCallCenterStabs()
        {
            var unitOfficeStub = UnitStub.Create(UnitType.Office);
            var unitCallCenterStub = UnitStub.Create(UnitType.CallCenter);
            var department = new DepartmentFake();
            department.AddUnit(unitOfficeStub);
            department.AddUnit(unitCallCenterStub);

            department.GetAllUnitsNames();

            Assert.Equal(1, unitCallCenterStub.ToStringCallCenterCounter);
            Assert.Equal(1, unitOfficeStub.ToStringOfficeCounter);
        }

        #endregion
        
        #region BehaviorTests
        
        [Fact]
        public void ShouldCallToStringOffice_WhenUnitTypeIsOfficeMock()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.Office);
            var department = new DepartmentFake();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitsNames();

            unitMock.Verify(x => x.ToStringOffice(), Times.Once);
        }
        
        [Fact]
        public void ShouldCallToStringPizzeria_WhenUnitTypeIsPizzeriaMock()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.Pizzeria);
            var department = new DepartmentFake();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitsNames();

            unitMock.Verify(x => x.ToStringPizzeria(), Times.Once);
        }
        
        [Fact]
        public void ShouldCallToStringCallCenter_WhenUnitTypeIsCallCenterMock()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.CallCenter);
            var department = new DepartmentFake();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitsNames();

            unitMock.Verify(x => x.ToStringCallCenter(), Times.Once);
        }
        
        [Fact]
        public void ShouldCallToStringWarehouse_WhenUnitTypeIsWarehouseMock()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.Warehouse);
            var department = new DepartmentFake();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitsNames();

            unitMock.Verify(x => x.ToStringWarehouse(), Times.Once);
        }
        
        [Fact]
        public void ShouldCallToStringServiceDelivery_WhenUnitTypeIsServiceDeliveryMock()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.ServiceDelivery);
            var department = new DepartmentFake();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitsNames();

            unitMock.Verify(x => x.ToStringServiceDelivery(), Times.Once);
        }
                
        [Fact]
        public void ShouldCallToString_WhenUnitTypeIsFactorySemisMock()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.FactorySemis);
            var department = new DepartmentFake();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitsNames();

            unitMock.Verify(x => x.ToString(), Times.Once);
        }

        [Fact]
        public void ShouldCallSpecificToString_WhenUnitTypeIsOfficeAndCallCenterMock()
        {
            var unitOfficeMock = new Mock<Unit>();
            var unitCallCenterMock = new Mock<Unit>();
            unitOfficeMock.SetupGet(x => x.Type).Returns(UnitType.Office);
            unitCallCenterMock.SetupGet(x => x.Type).Returns(UnitType.CallCenter);
            var department = new DepartmentFake();
            department.AddUnit(unitOfficeMock.Object);
            department.AddUnit(unitCallCenterMock.Object);

            department.GetAllUnitsNames();

            unitOfficeMock.Verify(x => x.ToStringOffice(), Times.Once);
            unitCallCenterMock.Verify(x => x.ToStringCallCenter(), Times.Once);
        }
        
        #endregion
    }
}
