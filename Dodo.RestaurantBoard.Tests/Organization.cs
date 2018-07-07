using Dodo.Core.DomainModel.Management.Organizations;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class Organization
    {
        [Fact]
        public void ShortHeadManagerName_HasSurnameWithInitials_WhenOrdinaryFullName()
        {
            var pizzeria = new OrganizationStab("Иванов Иван Иванович");

            var shortManagerName = pizzeria.ShortHeadManagerName;

            Assert.Equal("Иванов И.И.", shortManagerName);
        }
    }
}