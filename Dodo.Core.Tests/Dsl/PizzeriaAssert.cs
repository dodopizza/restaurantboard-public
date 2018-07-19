using System;
using Dodo.Core.DomainModel.Departments.Units;
using NUnit.Framework;

namespace Dodo.Core.Tests.Dsl
{
    public class PizzeriaAssert
    {
        private readonly Pizzeria _pizzeria;

        public PizzeriaAssert(Pizzeria pizzeria)
        {
            _pizzeria = pizzeria;
        }

        public void SpecifiedDateFromThePastIsNotValid(DateTime currentDate)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _pizzeria.GetYearsOld(currentDate));
        }
    }
}