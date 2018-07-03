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
<<<<<<< HEAD
        public void DepartmentsStructureServiceTests_GetPizzeria_Pizzeria_PizzaIsNotNull()
        {
            var service = departmentsStructureService;

            var pizzeria = service.GetPizzeriaOrCache(1);

            Assert.NotNull(pizzeria);
        }
=======
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
>>>>>>> origin/HW02_Aleksey_Anton

        [Fact]
        public void DepartmentsStructureServiceTests_GetDepartmentCountryName_Department_CountryAlwaysRussia()
        {
            var department = departmentsStructureService.GetDepartmentOrCache<Department>(sampleDepartmentId);

            var departmentCountryName = department.Country.Name;

            Assert.Equal("Russia", departmentCountryName);
        }

        [Fact]
        public void DepartmentsStructureServiceTests_GetDepartmentCountryCurrency_Department_CountryCurrencyAlwaysRubble()
        {
            var department = departmentsStructureService.GetDepartmentByUnitOrCache(sampleDepartmentId);

            var contryCurrency = department.Country.Currency;

            Assert.Equal(Currency.Ruble, contryCurrency);
        }



    }
}
