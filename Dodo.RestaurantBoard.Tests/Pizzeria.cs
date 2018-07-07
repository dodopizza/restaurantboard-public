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
        public void ShouldReturnAgeInYears_WhenGetYearsOld()
        {
            var nowDate = new DateTime(2016, 1, 1);
            var startDate = new DateTime(2011, 1, 1);
            var pizzeria = CreatePizzeriaWithCurrentBeginDateTimeWork(startDate);

            Assert.Equal(6, pizzeria.GetYearsOld(nowDate));
        }

        [Fact]
        public void ShouldReturnAgeInMonths_WhenGetMonthsOld()
        {
            var nowDate = new DateTime(2016, 1, 1);
            var startDate = new DateTime(2011, 1, 1);
            var pizzeria = CreatePizzeriaWithCurrentBeginDateTimeWork(startDate);

            Assert.Equal(72, pizzeria.GetMonthsOld(nowDate));
        }

        [Fact]
        public void ShouldBeZeroYearsOld_WhenOpenInFuture()
        {
            var pizzeria = CreatePizzeriaWithCurrentBeginDateTimeWork(new DateTime(2020, 1, 1));

            Assert.Equal(0, pizzeria.GetYearsOld(new DateTime(2018, 1, 6)));
        }

        [Fact]
        public void ShouldBeZeroMonthsOld_WhenOpenInFuture()
        {
            var pizzeria = CreatePizzeriaWithCurrentBeginDateTimeWork(new DateTime(2020, 1, 1));

            Assert.Equal(0, pizzeria.GetMonthsOld(new DateTime(2018, 1, 6)));
        }
    }
}
