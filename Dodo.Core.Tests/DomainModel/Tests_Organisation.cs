using Dodo.Core.DomainModel.Management.Organizations;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class Tests_Organisation
    {
        [Fact]
        public void ShortHeadManagerName_ShouldBecomeSurnameWithInitials_WhenFullNameContainsALotOfDotsAndSpaces()
        {
            var organization = new Organization("Гендольф.....Антон     Борисович");

            var surnameWithInitials = organization.ShortHeadManagerName;

            Assert.Equal("Гендольф А.Б.", surnameWithInitials);
        }

        [Fact]
        public void ShortHeadManagerName_ShouldBecomeSurnameWithInitials_WhenOrdinaryFullName()
        {
            var organization = new Organization("Гендольф Антон Борисович");

            var surnameWithInitials = organization.ShortHeadManagerName;

            Assert.Equal("Гендольф А.Б.", surnameWithInitials);
        }

        [Fact]
        public void ShortHeadManagerName_ShouldBeEmpty_WhenFullNameIsEmpty()
        {
            var organization = new Organization("");

            var surnameWithInitials = organization.ShortHeadManagerName;

            Assert.Equal("", surnameWithInitials);
        }
    }
}