using Dodo.Core.DomainModel.Departments;
using Dodo.Core.Tests.DomainModel.DSL;
using Moq;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class Tests_Unit
    {
        #region StateTests

        [Fact]
        public void NameShouldContainsOffice_WhenUnitTypeIsOffice()
        {
            var unit = new Unit(UnitType.Office);

            unit.GetName();

            Assert.Equal("Type: Office", unit.Name);
        }

        [Fact]
        public void NameShouldContainsPizzeria_WhenUnitTypeIsPizzeria()
        {
            var unit = new Unit(UnitType.Pizzeria);

            unit.GetName();

            Assert.Equal("Type: Pizzeria", unit.Name);
        }

        [Fact]
        public void NameShouldContainsCallCenter_WhenUnitTypeIsCallCenter()
        {
            var unit = new Unit(UnitType.CallCenter);

            unit.GetName();

            Assert.Equal("Type: CallCenter", unit.Name);
        }

        [Fact]
        public void NameShouldContainsWarehouse_WhenUnitTypeIsWarehouse()
        {
            var unit = new Unit(UnitType.Warehouse);

            unit.GetName();

            Assert.Equal("Type: Warehouse", unit.Name);
        }

        [Fact]
        public void NameShouldContainsServiceDelivery_WhenUnitTypeIsServiceDelivery()
        {
            var unit = new Unit(UnitType.ServiceDelivery);

            unit.GetName();

            Assert.Equal("Type: ServiceDelivery", unit.Name);
        }

        [Fact]
        public void NameShouldContainsUnknown_WhenUnitTypeIsFactorySemis()
        {
            var unit = new Unit(UnitType.FactorySemis);

            unit.GetName();

            Assert.Equal("Type: Unknown", unit.Name);
        }

        #endregion
        
        #region BehaviorTests

        [Fact]
        public void WithTypeOffice_ShouldCallToStringOffice_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = Create.UnitMock.WithType(UnitType.Office).Please();
            var department = Create.Department.WithUnit(unitMock.Object).Please();

            department.GetAllUnitNames();

            AssertThat(unitMock).ToStringOfficeMethod();
        }

        [Fact]
        public void WithTypePizzeria_ShouldCallToStringPizzeria_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = Create.UnitMock.WithType(UnitType.Pizzeria).Please();
            var department = Create.Department.WithUnit(unitMock.Object).Please();

            department.GetAllUnitNames();

            AssertThat(unitMock).ToStringPizzeriaMethod();
        }

        [Fact]
        public void WithTypeCallCenter_ShouldCallToStringCallCenter_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = Create.UnitMock.WithType(UnitType.CallCenter).Please();
            var department = Create.Department.WithUnit(unitMock.Object).Please();

            department.GetAllUnitNames();

            AssertThat(unitMock).ToStringCallCenterMethod();
        }

        [Fact]
        public void WithTypeWarehouse_ShouldCallToStringWarehouse_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = Create.UnitMock.WithType(UnitType.Warehouse).Please();
            var department = Create.Department.WithUnit(unitMock.Object).Please();

            department.GetAllUnitNames();

            AssertThat(unitMock).ToStringWarehouseMethod();
        }

        [Fact]
        public void WithTypeServiceDelivery_ShouldCallToStringServiceDelivery_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = Create.UnitMock.WithType(UnitType.ServiceDelivery).Please();
            var department = Create.Department.WithUnit(unitMock.Object).Please();

            department.GetAllUnitNames();

            AssertThat(unitMock).ToStringServiceDeliveryMethod();
        }

        [Fact]
        public void WithTypeFactorySemis_ShouldCallToString_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = Create.UnitMock.WithType(UnitType.FactorySemis).Please();
            var department = Create.Department.WithUnit(unitMock.Object).Please();

            department.GetAllUnitNames();

            AssertThat(unitMock).ToStringMethod();
        }

        #endregion

        private UnitAssertBuilder AssertThat(Mock<Unit> UnitMock)
        {
            return new UnitAssertBuilder(UnitMock);
        }
    }
}