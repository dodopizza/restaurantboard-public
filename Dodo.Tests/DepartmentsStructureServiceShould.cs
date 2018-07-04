using Dodo.Core.DomainModel.Finance;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Xunit;

namespace Dodo.Tests
{
    public class DepartmentsStructureServiceShould
    {
        [Theory]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void ReturnRussianCityDepartmentWithAnyUnitId(int unitId)
        {
            IDepartmentsStructureService testedService = new DepartmentsStructureService();

            var actualDepartment = testedService.GetDepartmentByUnitOrCache(unitId);

            Assert.Equal("Russia", actualDepartment.Country.Name);
            Assert.Equal("+7", actualDepartment.Country.PhoneCode);
            Assert.Equal(Currency.Ruble, actualDepartment.Country.Currency);
        }
    }
}
