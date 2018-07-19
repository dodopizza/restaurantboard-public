using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Tests.Dsl;
using NUnit.Framework;
using Tests.Dsl;

namespace Tests
{
    public class PizzeriaTests
    {
        [Test]
        public void GetYearsOld_ReturnsAges_ForPizzeria()
        {
            var pizzeria = Create.Pizzeria
                .WhichBeginsWorkAt(10.July(2008))
                .Please();

            var actualYears = pizzeria.GetYearsOld(09.July(2020));

            Assert.AreEqual(12, actualYears);
        }

        [Test]
        public void GetYearsOld_ShouldReturnsZero_WhenBeginDateTimeWorkIsUnknown()
        {
            var pizzeria = Create.Pizzeria
                .WhichOpenningDateIsNotApproved()
                .Please();

            var actualYears = pizzeria.GetYearsOld(09.July(2020));

            Assert.AreEqual(0, actualYears);
        }

        [Test]
        public void GetYearsOld_ShouldThrowArgumentOutOfRangeException_WhenCurrentDateBeforeBeginDateTimeWork()
        {
            var pizzeria = Create.Pizzeria
                .WhichBeginsWorkAt(10.July(2008))
                .Please();

            AssertThat(pizzeria).SpecifiedDateFromThePastIsNotValid(09.July(1999));
        }

        private static PizzeriaAssert AssertThat(Pizzeria pizzeria)
        {
            return new PizzeriaAssert(pizzeria);
        }
    }
}