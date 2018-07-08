using Xunit;
using System;
using Dodo.Core.DomainModel.Departments;

namespace Dodo.Core.Test
{
        public class DepartmentCultureDataTest
    {

        [Fact]
        public void WhenCreateDepartmentCultureData_ItIsNotFilled()
        {
            var departmentCultureData = new DepartmentCultureData();
             
            Assert.False(departmentCultureData.IsFilled());
        }

        [Fact]
        public void WhenDepartmentCultureDataGotName_ItIsFilled()
        {
            var departmentCultureData = new DepartmentCultureData();
            departmentCultureData.Name = "ru-RU";

            Assert.True(departmentCultureData.IsFilled());            
        }

    }
}