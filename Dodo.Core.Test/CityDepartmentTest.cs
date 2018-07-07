using Xunit;
using System;
using Dodo.Core.DomainModel.Departments.Departments;

namespace Dodo.Core.Test
{
    public class CityDepartmentTest
    {
        [Fact]
        public void WhenCityDepartmentConvertsToLocalDateTime_Null_ResultIsNull() 
        {
            cityDepartment = new CityDepartment();
            dataTimeToConverts = null;

            var result = cityDepartment.ToLocalDateTime(dataTimeToConverts);

            Assert.Null(result);
        }

        [Fact]
        public void WhenCityDepartmentCallToString_Result—omposedCityDepartmentNameTypeState()
        {
            cityDepartment = new CityDepartment();
            var relevantData = $"{cityDepartment.Name} Type: {cityDepartment.Type} State: {cityDepartment.State}";

            var result = cityDepartment.ToString();

            Assert.Equal(result, relevantData);
        }

        [Fact]
        public void WhenCityDepartmentGetUtcDateTime_CityDepartmentTimeZoneUtcOffsetSetupInOursZone__ResultHourEqualOursTimeZoneHour()
        {
            cityDepartment = new CityDepartment();
            myTimeZoneUTCOffset = 180;
            cityDepartment.TimeZoneUTCOffset = myTimeZoneUTCOffset;
            var oursTimeZone = DateTime.UtcNow;

            var result = cityDepartment.GetUtcDateTime(DateTime.Now);
            
            Assert.Equal(result.Hour, OursTimeZone.Hour);
        }

        [Fact]
        public void WhenCityDepartmentCallCurrentDateWithTimeZoneUtcOffset_CityDepartmentTimeZoneUtcOffsetSetupInOursZone_ResultDateEqualOursDate()
        {
            cityDepartment = new CityDepartment();
            myTimeZoneUTCOffset = 180;
            cityDepartment.TimeZoneUTCOffset = myTimeZoneUTCOffset;
            var oursTimeZoneData = DateTime.Now.Date;

            var result = cityDepartment.CurrentDate;

            Assert.Equal(result, oursTimeZoneData);
        }

        [Fact]
        public void WhenCityDepartmenCreate_UuidIsNull()
        {
            cityDepartment = new CityDepartment();

            Assert.Null(cityDepartment.Uuid);
        }
    }
}
