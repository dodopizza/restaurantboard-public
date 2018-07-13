using Dodo.Core.DomainModel.Departments;
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
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.Office);
            var department = new Department();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitNames();

            unitMock.Verify(x => x.ToStringOffice(), Times.Once);
        }

        [Fact]
        public void WithTypePizzeria_ShouldCallToStringPizzeria_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.Pizzeria);
            var department = new Department();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitNames();

            unitMock.Verify(x => x.ToStringPizzeria(), Times.Once);
        }

        [Fact]
        public void WithTypeCallCenter_ShouldCallToStringCallCenter_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.CallCenter);
            var department = new Department();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitNames();

            unitMock.Verify(x => x.ToStringCallCenter(), Times.Once);
        }

        [Fact]
        public void WithTypeWarehouse_ShouldCallToStringWarehouse_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.Warehouse);
            var department = new Department();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitNames();

            unitMock.Verify(x => x.ToStringWarehouse(), Times.Once);
        }

        [Fact]
        public void WithTypeServiceDelivery_ShouldCallToStringServiceDelivery_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.ServiceDelivery);
            var department = new Department();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitNames();

            unitMock.Verify(x => x.ToStringServiceDelivery(), Times.Once);
        }

        [Fact]
        public void WithTypeFactorySemis_ShouldCallToString_WhenDepartmentGetAllUnitNames()
        {
            var unitMock = new Mock<Unit>();
            unitMock.SetupGet(x => x.Type).Returns(UnitType.FactorySemis);
            var department = new Department();
            department.AddUnit(unitMock.Object);

            department.GetAllUnitNames();

            unitMock.Verify(x => x.ToString(), Times.Once);
        }

        #endregion
    }
}