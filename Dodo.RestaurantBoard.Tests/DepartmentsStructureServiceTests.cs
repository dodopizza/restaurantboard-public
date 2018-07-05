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
        int sampleDepartmentId = 1;

        public DepartmentsStructureServiceTests()
        {
            departmentsStructureService = new DepartmentsStructureService();

        }

        [Fact]
        public void GetPizzeria_Department_PizzaIsNotNull()
        {
            var service = departmentsStructureService;

            var pizzeria = service.GetPizzeriaOrCache(1);

            Assert.NotNull(pizzeria);
        }


        [Fact]
        public void GetDepartmentCountryName_Department_CountryAlwaysRussia()
        {
            var department = departmentsStructureService.GetDepartmentOrCache<Department>(sampleDepartmentId);

            var departmentCountryName = department.Country.Name;

            Assert.Equal("Russia", departmentCountryName);
        }

        [Fact]
        public void GetDepartmentCountryCurrency_Department_CountryCurrencyAlwaysRubble()
        {
            var department = departmentsStructureService.GetDepartmentByUnitOrCache(sampleDepartmentId);

            var contryCurrency = department.Country.Currency;

            Assert.Equal(Currency.Ruble, contryCurrency);
        }



    }
}
