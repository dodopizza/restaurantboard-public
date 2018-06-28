using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Management.Organizations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dodo.Core.Tests
{
    [TestClass]
    public class PizzeriaTest
    {
        [TestMethod]
        public void GetYearsOld_NotOpenedPizzeria()
        {
            var currentDate = DateTime.MinValue;
            DateTime? beginDateTimeWork = null;
            var sut = new Pizzeria(0, Common.Uuid.Empty, "", "", "", DomainModel.Departments.UnitApprove.Approved, DomainModel.Departments.UnitState.Close, 0, Common.Uuid.Empty, 0, mockOrganization, 0, beginDateTimeWork, "", null, null, null, ClientTreatment.DefaultName, false, mockPizzeriaFormat);
            int expected = 0;

            int result = sut.GetYearsOld(currentDate);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GetYearsOld_PizzeriaOpenedYearBefore()
        {
            var beginDateTimeWork = DateTime.MinValue;
            var currentDate = beginDateTimeWork.AddYears(1);
            var sut = new Pizzeria(0, Common.Uuid.Empty, "", "", "", DomainModel.Departments.UnitApprove.Approved, DomainModel.Departments.UnitState.Close, 0, Common.Uuid.Empty, 0, mockOrganization, 0, beginDateTimeWork, "", null, null, null, ClientTreatment.DefaultName, false, mockPizzeriaFormat);
            int expected = 1;

            int result = sut.GetYearsOld(currentDate);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GetMonthsOld_NotOpenedPizzeria()
        {
            var currentDate = DateTime.MinValue;
            DateTime? beginDateTimeWork = null;
            var sut = new Pizzeria(0, Common.Uuid.Empty, "", "", "", DomainModel.Departments.UnitApprove.Approved, DomainModel.Departments.UnitState.Close, 0, Common.Uuid.Empty, 0, mockOrganization, 0, beginDateTimeWork, "", null, null, null, ClientTreatment.DefaultName, false, mockPizzeriaFormat);
            int expected = 0;

            int result = sut.GetMonthsOld(currentDate);

            Assert.AreEqual(result, expected);
        }

        [TestMethod]
        public void GetMonthsOld_PizzeriaOpenedMonthBefore()
        {
            var beginDateTimeWork = DateTime.MinValue;
            var currentDate = beginDateTimeWork.AddMonths(1);
            var sut = new Pizzeria(0, Common.Uuid.Empty, "", "", "", DomainModel.Departments.UnitApprove.Approved, DomainModel.Departments.UnitState.Close, 0, Common.Uuid.Empty, 0, mockOrganization, 0, beginDateTimeWork, "", null, null, null, ClientTreatment.DefaultName, false, mockPizzeriaFormat);
            int expected = 1;

            int result = sut.GetMonthsOld(currentDate);

            Assert.AreEqual(expected, result);
        }

        OrganizationShortInfo mockOrganization = new DomainModel.Management.Organizations.OrganizationShortInfo(0, "", "", null, "", "", "", 0, "", "", "");
        PizzeriaFormat mockPizzeriaFormat = new PizzeriaFormat(0, "", "");
    }
}
