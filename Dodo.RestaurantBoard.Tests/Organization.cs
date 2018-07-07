using Dodo.Core.DomainModel.Management.Organizations;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class Organization
    {
        [Fact]
        public void ShouldHasSurnameWithInitials_WhenShortHeadManagerNameHasOrdinaryFullName()
        {
            var pizzeria = new OrganizationStab("Иванов Иван Иванович");

            Assert.Equal("Иванов И.И.", pizzeria.ShortHeadManagerName);
        }
    }
}