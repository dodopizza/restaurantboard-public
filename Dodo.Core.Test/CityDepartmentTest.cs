using Xunit;
using System;
using Dodo.Core.DomainModel.Departments.Departments;

namespace Dodo.Core.Test
{
    public class CityDepartmentTest
    {        
        [Fact]
        public void WhenCityDepartmentConvertsToLocalDateTime_Null_IttIsNull() 
        {
            var cityDepartment = new CityDepartment();
            DateTime? dataTimeToConverts = null;

            var result = cityDepartment.ToLocalDateTime(dataTimeToConverts);

            Assert.Null(result);
        }

        [Fact]
        public void WhenCityDepartmentCallToString_ItComposedCityDepartmentNameTypeState()
        {
            var cityDepartment = new CityDepartment();

            var result = cityDepartment.ToString();

            Assert.Equal(result, " Type: CentralOffice State: Close");
        }

        [Fact]
        public void WhenCityDepartmenCreate_UuidIsNull()
        {
            var cityDepartment = new CityDepartment();

            Assert.Null(cityDepartment.Uuid);
        }
    }
}
