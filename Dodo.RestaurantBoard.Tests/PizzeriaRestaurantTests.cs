using Dodo.Core.DomainModel.Management.Organizations;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class PizzeriaRestaurantTests
    {
        [Fact]
        public void GetShortHeadManagerName_Pizzeria_ReturnManagerInitials()
        {
            var pizzeria = new PizzeriaRestaurant("Test Name");

            var shortManagerName = pizzeria.ShortHeadManagerName;

            Assert.Equal("Test N.", shortManagerName);
        }
    }
}