using Xunit;
using System;
using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Departments;

namespace Dodo.Core.Test
{
    public class CityDepartmentTest
    {

        [Fact]
        public void WhenCityDepartmentConvertsToLocalDateTime_Null_ItIsNull()
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
            var relevantData = $"{cityDepartment.Name} Type: {cityDepartment.Type} State: {cityDepartment.State}";

            var result = cityDepartment.ToString();

            Assert.Equal(result, relevantData);
        }

        [Fact]
        public void WhenCityDepartmenCreate_UuidIsNull()
        {
            var cityDepartment = new CityDepartment();

            Assert.Null(cityDepartment.Uuid);
        }

    }

    public class DepartmentCultureDataTest
    {

        [Fact]
        public void WhenDepartmentCultureDataCreate_ItIsEmpty()
        {
            var departmentCultureData = new DepartmentCultureData();

            Assert.True(!departmentCultureData.IsFilled());
        }

        [Fact]
        public void WhenDepartmentCultureDataGotName_ItIsFilled()
        {
            var departmentCultureData = new DepartmentCultureData();
            departmentCultureData.Name = "uru_URU";

            Assert.True(departmentCultureData.IsFilled());
        }

    }
}
