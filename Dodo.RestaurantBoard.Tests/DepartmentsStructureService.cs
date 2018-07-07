using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using System;
using System.Diagnostics;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class DepartmentsStructureService
    {

        IDepartmentsStructureService departmentsStructureService;
        

        public DepartmentsStructureService()
        {
            departmentsStructureService = new Domain.Services.DepartmentsStructureService();

        }

        [Fact]
        public void WhenGetPizzeria_ItNotNull()
        {
            var service = departmentsStructureService;

            var pizzeria = service.GetPizzeriaOrCache(1);

            Assert.NotNull(pizzeria);
        }


        [Fact]
        public void WhenGetDepartment_CountryNameShouldBeRussia()
        {
            int sampleDepartmentId = 1;
            var department = departmentsStructureService.GetDepartmentOrCache<Department>(sampleDepartmentId);

            var departmentCountryName = department.Country.Name;

            Assert.Equal("Russia", departmentCountryName);
        }

        [Fact]
        public void WhenGetDepartment_CountryCurrencyShouldBeRuble()
        {
            int sampleDepartmentId = 1;
            var department = departmentsStructureService.GetDepartmentByUnitOrCache(sampleDepartmentId);

            var contryCurrency = department.Country.Currency;

            Assert.Equal(Currency.Ruble, contryCurrency);
        }
    }
}
