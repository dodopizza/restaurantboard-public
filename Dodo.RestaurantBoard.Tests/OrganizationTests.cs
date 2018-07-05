using Dodo.Core.DomainModel.Management.Organizations;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class OrganizationTests
    {
        [Fact]
        public void GetPizzeriaHeadManagerShortName_PizzeriaWithHeadManagerName_HeadManagerShortName()
        {
            var pizzeriaRestaurant = new PizzeriaRestaurant("Test Name");

            var shortManagerName = pizzeriaRestaurant.ShortHeadManagerName;

            Assert.Equal("Test N.", shortManagerName);
        }
    }
}