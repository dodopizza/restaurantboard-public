using Dodo.Core.DomainModel.Management.Organizations;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class OrganizationTests
    {
        [Fact]
        public void ShouldBeCorrectShortHeadManagerName()
        {
            var prc = new PizzeriaRestaurant("Test Name");
            Assert.Equal("Test N.", prc.ShortHeadManagerName);
        }
    }
}