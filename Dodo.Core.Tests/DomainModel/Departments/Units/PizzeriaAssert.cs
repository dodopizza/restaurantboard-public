using System;
using Dodo.Core.DomainModel.Departments.Units;
using Xunit;

namespace Dodo.Core.Tests.DomainModel.Departments
{
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

        public void HasAgeOf(int expectedAge, string kind)
        {
            if (currentDateTime == null)
            {
                throw new Exception("Setup incomplete: set not-null currentDateTime");
            }
            var actualAge = kind == In.Years ? _pizzeria.GetYearsOld(currentDateTime.Value) : _pizzeria.GetMonthsOld(currentDateTime.Value);

            Assert.Equal(expectedAge, actualAge);
        }

        public PizzeriaAssert At(DateTime date)
        {
            currentDateTime = date;
            return this;
        }

        public void HasNoFormat()
        {
            Assert.Null(_pizzeria.Format);
        }
    }
}