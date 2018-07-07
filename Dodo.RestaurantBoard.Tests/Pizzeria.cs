using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.Management.Organizations;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Domain = Dodo.Core.DomainModel.Departments.Units;

namespace Dodo.RestaurantBoard.Tests
{
    public class Pizzeria
    {


        private Domain::Pizzeria CreatePizzeriaWithCurrentBeginDateTimeWork(DateTime startDate)
        {
            var orgInfo = new OrganizationShortInfo(0, string.Empty, string.Empty, OrganizationType.Rus_OOO, string.Empty, string.Empty, string.Empty, 1, string.Empty, string.Empty, string.Empty);
            return new Domain::Pizzeria(29, new Core.Common.Uuid("000D3A240C719A8711E68ABA13F83227"), "Сык-1", string.Empty,
            string.Empty, UnitApprove.Approved, UnitState.Open, 2, new Core.Common.Uuid("000D3A240C719A8711E68ABA13FC4A39"), 1,
            orgInfo, 100, startDate, "Gay", true, 1, 1, ClientTreatment.Name,
            true, new PizzeriaFormat(0, string.Empty, string.Empty));
        }

        [Fact]
        public void WhenGetYearsOld_ShouldReturnAgeInYears()
        {
            var nowDate = new DateTime(2016, 1, 1);
            var startDate = new DateTime(2011, 1, 1);
            var pizzeria = CreatePizzeriaWithCurrentBeginDateTimeWork(startDate);

            var yearsOld = pizzeria.GetYearsOld(nowDate);

            Assert.Equal(6, yearsOld);
        }

        [Fact]
        public void WhenGetMonthsOld_ShouldReturnAgeInMonths()
        {
            var nowDate = new DateTime(2016, 1, 1);
            var startDate = new DateTime(2011, 1, 1);
            var pizzeria = CreatePizzeriaWithCurrentBeginDateTimeWork(startDate);

            var monthOld = pizzeria.GetMonthsOld(nowDate);

            Assert.Equal(72, monthOld);
        }

        [Fact]
        public void WhenPizzeriaOpenInFuture_ShouldBeZeroYearsOld()
        {
            var pizzeria = CreatePizzeriaWithCurrentBeginDateTimeWork(new DateTime(2020, 1, 1));

            var totalYearsOld = pizzeria.GetYearsOld(new DateTime(2018, 1, 6));

            Assert.Equal(0, totalYearsOld);
        }

        [Fact]
        public void WhenPizzeriaOpenInFuture_ShouldBeZeroMonthsOld()
        {
            var pizzeria = CreatePizzeriaWithCurrentBeginDateTimeWork(new DateTime(2020, 1, 1));
            
            var totalMonthsOld = pizzeria.GetMonthsOld(new DateTime(2018, 1, 6));

            Assert.Equal(0, totalMonthsOld);
        }
    }
}
