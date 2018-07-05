using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
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
        public void GetYearsOld_WhenNotOpened_ThenZero()
        {
            // Arrange
            var currentDate = DateTime.MinValue;
            Pizzeria pizzeria = CreatePizzeria(beginDateTimeWork: null);

            // Act
            int yearsOld = pizzeria.GetYearsOld(currentDate);

            // Assert
            Assert.AreEqual(0, yearsOld);
        }

        [TestMethod]
        public void GetYearsOld_WhenOpenedYearAgo_ThenOne()
        {
            // Arrange
            var beginDateTimeWork = DateTime.MinValue;
            var currentDate = beginDateTimeWork.AddYears(1);
            var pizzeria = CreatePizzeria(beginDateTimeWork: beginDateTimeWork);

            // Act
            int yearsOld = pizzeria.GetYearsOld(currentDate);

            // Assert
            Assert.AreEqual(1, yearsOld);
        }

        [TestMethod]
        public void GetMonthsOld_WhenNotOpened_ThenZero()
        {
            // Arrange
            var currentDate = DateTime.MinValue;
            var pizzeria = CreatePizzeria(beginDateTimeWork: null);

            // Act
            int monthsOld = pizzeria.GetMonthsOld(currentDate);

            // Assert
            Assert.AreEqual(0, monthsOld);
        }

        [TestMethod]
        public void GetMonthsOld_WhenOpenedMonthAgo_ThenOne()
        {
            // Arrange
            var beginDateTimeWork = DateTime.MinValue;
            var currentDate = beginDateTimeWork.AddMonths(1);
            var pizzeria = CreatePizzeria(beginDateTimeWork: beginDateTimeWork);

            // Act
            int monthsOld = pizzeria.GetMonthsOld(currentDate);

            // Assert
            Assert.AreEqual(1, monthsOld);
        }

        [TestMethod]
        public void IsExistsPizzeriaFormat_WhenNoFormat_ThenFalse()
        {
            // Arrange
            var pizzeria = CreatePizzeria(pizzeriaFormat: null);

            // Act & Assert
            Assert.IsFalse(pizzeria.IsExistsPizzeriaFormat);
        }

        [TestMethod]
        public void IsExistsPizzeriaFormat_WhenHasFormat_ThenTrue()
        {
            // Arrange
            PizzeriaFormat mockPizzeriaFormat = new PizzeriaFormat(0, "", "");
            var pizzeria = CreatePizzeria(pizzeriaFormat: mockPizzeriaFormat);

            // Act & Assert
            Assert.IsTrue(pizzeria.IsExistsPizzeriaFormat);
        }

        private Pizzeria CreatePizzeria(
            Int32 id = 0, Uuid uuid = null, String name = "", String alias = "", String translitAlias = "", UnitApprove approve = new UnitApprove(), UnitState state = new UnitState(),
            Int32 departmentId = 0, Uuid departmentUuid = null, Int32 countryId = 0, OrganizationShortInfo organization = null, Double square = 0,
            DateTime? beginDateTimeWork = null, String orientation = "", Boolean? cardPaymentPickup = null, Decimal? coordinateX = null, Decimal? coordinateY = null,
            ClientTreatment clientTreatment = new ClientTreatment(), Boolean terminalAtCourier = false, PizzeriaFormat pizzeriaFormat = null)
        {
            return new Pizzeria(
                id, uuid, name, alias, translitAlias, approve, state,
                departmentId, departmentUuid, countryId, organization, square,
                beginDateTimeWork, orientation, cardPaymentPickup, coordinateX, coordinateY,
                clientTreatment, terminalAtCourier, pizzeriaFormat);
        }
    }
}
