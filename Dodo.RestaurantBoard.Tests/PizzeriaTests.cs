using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Management.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class PizzeriaTests
    {


        public Pizzeria CreatePizzeriaWithCurrentDate(DateTime startDate)
        {
            var orgInfo = new OrganizationShortInfo(0, string.Empty, string.Empty, OrganizationType.Rus_OOO, string.Empty, string.Empty, string.Empty, 1, string.Empty, string.Empty, string.Empty);
            return new Pizzeria(29, new Uuid("000D3A240C719A8711E68ABA13F83227"), "Сык-1", string.Empty,
            string.Empty, UnitApprove.Approved, UnitState.Open, 2, new Uuid("000D3A240C719A8711E68ABA13FC4A39"), 1,
            orgInfo, 100, startDate, "Gay", true, 1, 1, ClientTreatment.Name,
            true, new PizzeriaFormat(0, string.Empty, string.Empty));
        }

        [Theory]
        [MemberData(nameof(CheckYearsData))]
        public void VerifyYearsOld(DateTime startDate, DateTime now, int expertedYears)
        {
            var pizzeria = CreatePizzeriaWithCurrentDate(startDate);

            Assert.Equal(expertedYears, pizzeria.GetYearsOld(now));
        }

        public static IEnumerable<object[]> CheckYearsData = new List<object[]>
        {
            new object[] {new DateTime(2011,1,1), new DateTime(2016, 1, 1), 6 },
            new object[] {new DateTime(2011,1,1), new DateTime(2018, 2, 1), 8 },
            new object[] {new DateTime(2011,2,1), new DateTime(2021, 1, 1), 10 }
        };


        [Theory]
        [MemberData(nameof(CheckMonthsData))]
        public void VerifyMonthOld(DateTime startDate, DateTime now, int expectedMonths)
        {
            var pizzeria = CreatePizzeriaWithCurrentDate(startDate);

            Assert.Equal(expectedMonths, pizzeria.GetMonthsOld(now));
        }

        public static IEnumerable<object[]> CheckMonthsData = new List<object[]>
        {
            new object[] {new DateTime(2011,1,1), new DateTime(2016, 1, 1), 72 },
            new object[] {new DateTime(2011,1,1), new DateTime(2018, 2, 1), 97 },
            new object[] {new DateTime(2011,2,1), new DateTime(2021, 1, 1), 131 }
        };


        [Fact]
        public void ShouldCorrectlyHandleIncorredDatesInGetYears()
        {
            var pizzeria = CreatePizzeriaWithCurrentDate(new DateTime(2020, 1, 1));
            // А он не хэндлит. Поэтому тест не проходит.
            var totalYears = pizzeria.GetYearsOld(new DateTime(2018, 30, 6));
            Assert.Equal(0, totalYears);
            var totalMonth = pizzeria.GetMonthsOld(new DateTime(2018, 30, 6));
        }

        [Fact]
        public void ShouldCorrectlyHandleIncorredDatesInGetMonth()
        {
            var pizzeria = CreatePizzeriaWithCurrentDate(new DateTime(2020, 1, 1));
            // А он не хэндлит. Поэтому тест не проходит.
            var totalMonth = pizzeria.GetMonthsOld(new DateTime(2018, 30, 6));
            Assert.Equal(0, totalMonth);

        }



    }
}
