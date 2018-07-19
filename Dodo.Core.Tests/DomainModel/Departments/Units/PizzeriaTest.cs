using System;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Xunit;

namespace Dodo.Core.Tests.DomainModel.Departments
{
    public class PizzeriaTest
    {
        [Fact]
        public void IsZeroYearsOld_WhenPizzeriaIsNotOpened()
        {
            Pizzeria pizzeria = CreatePizzeria()
                .ThatIsNotOpened()
                .Please();
            
            PizzeriaAssert.That(pizzeria).At(DateTime.Now).HasAgeOf(0);
        }

        [Fact]
        public void IsOneYearOld_WhenOpenedYearFromViewDate()
        {
            Pizzeria pizzeria = CreatePizzeria()
                .ThatIsOpened(14.JulyOf(2019))
                .Please();
            
            PizzeriaAssert.That(pizzeria).At(14.JulyOf(2020)).HasAgeOf(1);
        }

        private PizzeriaBuilder CreatePizzeria()
        {
            return new PizzeriaBuilder();
        }
    }

    public static class IntegerExtensions
    {
        public static DateTime JulyOf(this int day, int year)
        {
            return new DateTime(year, 7, day);
        }
    }

    public class PizzeriaAssert
    {
        private Pizzeria _pizzeria;
        private DateTime? currentDateTime;

        private PizzeriaAssert(Pizzeria pizzeria)
        {
            _pizzeria = pizzeria;
        }

        public static PizzeriaAssert That(Pizzeria pizzeria)
        {
            return new PizzeriaAssert(pizzeria);
        }

        public void HasAgeOf(int age)
        {
            if (currentDateTime == null)
            {
                throw new Exception("Setup incomplete: set not-null currentDateTime");
            }
            Assert.Equal(age, _pizzeria.GetYearsOld(currentDateTime.Value));
        }

        public PizzeriaAssert At(DateTime date)
        {
            currentDateTime = date;
            return this;
        }
    }

    internal class PizzeriaBuilder
    {
        private DateTime? openingDate;

        public PizzeriaBuilder ThatIsNotOpened()
        {
            openingDate = null;
            return this;
        }

        public PizzeriaBuilder ThatIsOpened(DateTime date)
        {
            openingDate = date;
            return this;
        }

        public Pizzeria Please()
        {
            return new Pizzeria(0, null, "", null, "", UnitApprove.Approved, UnitState.Close, 0, null, 0, null, 0.0,
                openingDate,
                "", null, null, null, ClientTreatment.DefaultName, false, null);
        }
    }
}