using Dodo.Core.DomainModel.Management.Organizations;
using Dodo.Core.Tests.DomainModel.DSL;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class Tests_Organisation
    {
        [Fact]
        public void ShortHeadManagerName_ShouldBecomeSurnameWithInitials_WhenFullNameContainsALotOfDotsAndSpaces()
        {
            var organization = Create.Organization.WithHeadManagerName("Гендольф.....Антон     Борисович").Please();

            var surnameWithInitials = organization.ShortHeadManagerName;

            Assert.Equal("Гендольф А.Б.", surnameWithInitials);
        }

        [Fact]
        public void ShortHeadManagerName_ShouldBecomeSurnameWithInitials_WhenOrdinaryFullName()
        {
            var organization = Create.Organization.WithHeadManagerName("Гендольф Антон Борисович").Please();

            var surnameWithInitials = organization.ShortHeadManagerName;

            Assert.Equal("Гендольф А.Б.", surnameWithInitials);
        }

        [Fact]
        public void ShortHeadManagerName_ShouldBeEmpty_WhenFullNameIsEmpty()
        {
            var organization = Create.Organization.WithHeadManagerName("").Please();

            var surnameWithInitials = organization.ShortHeadManagerName;

            Assert.Equal("", surnameWithInitials);
        }
    }
}