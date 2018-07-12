using Dodo.Tests.DSL;
using NUnit.Framework;

namespace Dodo.Tests
{
    [TestFixture]
    public class RestaurantReadnessOrdersTests
    {
        private readonly ObjectMother _mother = new ObjectMother();

        [Test]
        public void WhenRestaurantReadnessOrderHasOddNumber_ThenColorIsRed()
        {
            var restaurantReadnessOrder = _mother.CreateRestaurantReadnessOrderWithNumber(1);

            Assert.AreEqual("red", restaurantReadnessOrder.Color);
        }

        [Test]
        public void WhenRestaurantReadnessOrderHasEvenNumber_ThenColorIsRed()
        {
            var restaurantReadnessOrder = _mother.CreateRestaurantReadnessOrderWithNumber(2);

            Assert.AreEqual("green", restaurantReadnessOrder.Color);
        }
    }
}