using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Xunit;
using FluentAssertions;

namespace Dodo.Tests
{
    public class DepartmentsStructureServiceTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void ReturnCityDepartment(int unitId)
        {
            IDepartmentsStructureService testedService = new DepartmentsStructureService();

            var expectedDepartment = new CityDepartment
            {
                Country = new Country(1, "Russia", "+7", null, string.Empty, Currency.Ruble, string.Empty)
            };

            var actualDepartment = testedService.GetDepartmentByUnitOrCache(unitId);

            actualDepartment.Should().BeEquivalentTo(expectedDepartment, 
                o => o.Excluding(x => x.CurrentDateTime).Excluding(x => x.CurrentDateTimeUtc));
        }
    }
}
