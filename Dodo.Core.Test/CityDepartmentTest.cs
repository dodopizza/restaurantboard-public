using Xunit;
using System;
using Dodo.Core.DomainModel.Departments.Departments;

namespace Dodo.Core.Test
{
    public class CityDepartmentTest
    {
        private CityDepartment _cityDepartment;

        public CityDepartmentTest()
        {
            _cityDepartment = new CityDepartment();
        }

        [Fact]
        public void WhenCityDepartment.—onvertsNullToLocalDateTime_ResultIsNull()
        {
            var restult = _cityDepartment.ToLocalDateTime(null);

            Assert.Null(restult);
        }

        [Fact]
        public void WhenCityDepartment.CallToString_Result—omposedTheRelevantData()
        {
            Assert.Equal(_cityDepartment.ToString(), $"{_cityDepartment.Name} Type: {_cityDepartment.Type} State: {_cityDepartment.State}");
        }

        [Fact]
        public void WhenCityDepartment.GetUtcDateTimeWithTimeZoneUtcOffsetSetupOnMe_HourEqualMyHour()
        {
            _cityDepartment.TimeZoneUTCOffset = 180;
            var dateUtc = _cityDepartment.GetUtcDateTime(DateTime.Now);
            Assert.Equal(dateUtc.Hour, DateTime.UtcNow.Hour);
        }

        [Fact]
        public void WhenCityDepartment.CallCurrentDateWithTimeZoneUtcOffsetSetupOnMe_DateEqualMyDate()
        {
            _cityDepartment.TimeZoneUTCOffset = 180;
            Assert.Equal(_cityDepartment.CurrentDate, DateTime.Now.Date);
        }

        [Fact]
        public void WhenCityDepartmenCreate.UuidIsNull()
        {
            Assert.Null(_cityDepartment.Uuid);
        }
    }
}
