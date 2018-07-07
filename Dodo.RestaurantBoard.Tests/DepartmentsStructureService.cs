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
        public void ShouldBeNotNull_WhenRequestingPizzeria()
        {
            var service = departmentsStructureService;

            var pizzeria = service.GetPizzeriaOrCache(1);

            Assert.NotNull(pizzeria);
        }


        [Fact]
        public void CountryNameShouldBeRussia_WhenRequestingDepartment()
        {
            var department = departmentsStructureService.GetDepartmentOrCache<Department>(departmentId:1);

            Assert.Equal("Russia", department.Country.Name);
        }

        [Fact]
        public void CountryCurrencyShouldBeRuble_WhenRequestingdDepartment()
        {
            var department = departmentsStructureService.GetDepartmentByUnitOrCache(unitId:1);

            Assert.Equal(Currency.Ruble, department.Country.Currency);
        }
    }
}
