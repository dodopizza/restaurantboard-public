using System;
using Dodo.Core.UnitTests.DSL;
using NUnit.Framework;

namespace Dodo.Core.UnitTests
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
        public void When_pass_date_greater_than_pizzeria_begin_date_returns_correct_pizzeria_age_in_years()
        {
            var pizzeriaBeginWorkDate = new DateTime(2010, 1, 2);
            var currentDate = new DateTime(2018, 1, 1);
            var pizzeria = _objectMother.CreatePizzeriaWithBeginDateTimeWork(pizzeriaBeginWorkDate);

            Assert.AreEqual(8, pizzeria.GetYearsOld(currentDate));
        }
    }
}