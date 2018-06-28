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
        public void When_pass_date_greater_than_pizzeria_begin_date_returns_age_in_years()
        {
            var pizzeriaBeginWorkDate = new DateTime(2010, 1, 2);
            var currentDate = new DateTime(2018, 1, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(pizzeriaBeginWorkDate);

            Assert.AreEqual(8, pizzeria.GetYearsOld(currentDate));
        }

        [Test]
        public void When_begin_date_is_null_returns_zero()
        {
            var currentDate = new DateTime(2018, 1, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(null);

            Assert.AreEqual(0, pizzeria.GetYearsOld(currentDate));
        }

        [Test]
        public void When_pass_date_less_then_begin_date_throws_exception()
        {
            var beginDate = new DateTime(2018, 5, 1);
            var currentDate = new DateTime(2018, 1, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(beginDate);

            Assert.Throws<ArgumentOutOfRangeException>(() => pizzeria.GetYearsOld(currentDate));
        }
        
        [Test]
        public void When_pass_date_greater_than_pizzeria_begin_date_less_than_year_returns_age_in_months()
        {
            var pizzeriaBeginWorkDate = new DateTime(2010, 1, 1);
            var currentDate = new DateTime(2010, 5, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(pizzeriaBeginWorkDate);

            Assert.AreEqual(4, pizzeria.GetMonthsOld(currentDate));
        }

        [Test]
        public void When_pass_date_greater_than_pizzeria_begin_date_more_than_year_returns_age_in_months()
        {
            var pizzeriaBeginWorkDate = new DateTime(2010, 1, 1);
            var currentDate = new DateTime(2012, 5, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(pizzeriaBeginWorkDate);

            Assert.AreEqual(28, pizzeria.GetMonthsOld(currentDate));
        }

        [Test]
        public void When_pass_date_less_then_begin_date_for_months_throws_exception()
        {
            var beginDate = new DateTime(2018, 5, 1);
            var currentDate = new DateTime(2018, 1, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(beginDate);

            Assert.Throws<ArgumentOutOfRangeException>(() => pizzeria.GetMonthsOld(currentDate));
        }
    }

}