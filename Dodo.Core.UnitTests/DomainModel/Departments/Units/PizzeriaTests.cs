using System;
using Dodo.Core.UnitTests.DSL;
using NUnit.Framework;

namespace Dodo.Core.UnitTests.DomainModel.Departments.Units
{
    [TestFixture]
    public class PizzeriaTests
    {
        private ObjectMother _objectMother;

        [SetUp]
        public void Setup()
        {
            _objectMother = new ObjectMother();
        }

        [Test]
        public void GetYearsOld_ReturnsAgeInYears_ForPizzeria()
        {
            var date = new DateTime(2018, 1, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(new DateTime(2010, 1, 2));

            var pizzeriaYearsOld = pizzeria.GetYearsOld(date);

            Assert.AreEqual(8, pizzeriaYearsOld);
        }

        [Test]
        public void GetYearsOld_ThrowsArgumentOutOfRangeException_ForDatesBeforePizzeriaOpening()
        {
            var date = new DateTime(2018, 1, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(new DateTime(2018, 5, 1));

            Assert.Throws<ArgumentOutOfRangeException>(() => pizzeria.GetYearsOld(date));
        }

        [Test]
        public void GetYearsOld_ReturnsZero_ForAnyDateWhenPizzeriaOpeningDateIsNotSet()
        {
            var date = new DateTime(2018, 1, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(null);

            var pizzeriaYearsOld = pizzeria.GetYearsOld(date);

            Assert.AreEqual(0, pizzeriaYearsOld);
        }

        [Test]
        public void GetMonthsOld_ReturnsAgeInMonthsForYoungPizzeria()
        {
            var date = new DateTime(2010, 5, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(new DateTime(2010, 1, 1));

            var pizzeriaMonthsOld = pizzeria.GetMonthsOld(date);

            Assert.AreEqual(4, pizzeriaMonthsOld);
        }

        [Test]
        public void GetMonthOld_ReturnsAgeInMonthsForOldPizzeria()
        {
            var date = new DateTime(2012, 5, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(new DateTime(2010, 1, 1));

            var pizzeriaMonthsOld = pizzeria.GetMonthsOld(date);

            Assert.AreEqual(28, pizzeriaMonthsOld);
        }

        [Test]
        public void GetMonthsOld_ThrowsArgumentOutOfRangeException_ForDatesBeforePizzeriaOpening()
        {
            var date = new DateTime(2018, 1, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(new DateTime(2018, 5, 1));

            Assert.Throws<ArgumentOutOfRangeException>(() => pizzeria.GetMonthsOld(date));
        }
    }

}