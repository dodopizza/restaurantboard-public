using Dodo.Tests.DSL;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NUnit.Framework;

namespace Dodo.Tests
{
    [TestFixture]
    public class RestaurantReadnessOrdersTests
    {
        private readonly ObjectMother _mother = new ObjectMother();

        // State
        [Test]
        public void WhenRestaurantReadnessOrderHasOddNumber_ThenColorIsRed()
        {
            var restaurantReadnessOrder = _mother.CreateRestaurantReadnessOrderWithNumber(1);

            var color = restaurantReadnessOrder.Color;

            Assert.AreEqual("red", color);
        }

        // State
        [Test]
        public void WhenRestaurantReadnessOrderHasEvenNumber_ThenColorIsRed()
        {
            var restaurantReadnessOrder = _mother.CreateRestaurantReadnessOrderWithNumber(2);

            var color = restaurantReadnessOrder.Color;

            Assert.AreEqual("green", color);
        }
    }
}