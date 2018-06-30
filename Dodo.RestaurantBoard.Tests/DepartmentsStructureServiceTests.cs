using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using System;
using System.Diagnostics;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class DepartmentsStructureServiceTests
    {

        IDepartmentsStructureService departmentsStructureService;

        public DepartmentsStructureServiceTests()
        {
            departmentsStructureService = new DepartmentsStructureService();

        }

        [Fact]
        public void PizzaIsNotNull()
        {
            var service = departmentsStructureService;
            var pizza = service.GetPizzeriaOrCache(1);
            Assert.NotNull(pizza);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]

        public void IsDepartmentCountryAlwaysRussia(int departmentId)
        {
            var department = departmentsStructureService.GetDepartmentOrCache<Department>(departmentId);
            Assert.Equal("Russia", department.Country.Name);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]

        public void IsDepartmentByUidCurrencyAlwaysRubble(int iud)
        {
            var department = departmentsStructureService.GetDepartmentByUnitOrCache(iud);
            Assert.Equal(Currency.Ruble, department.Country.Currency);
        }



    }
}
