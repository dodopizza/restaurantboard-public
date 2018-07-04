using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.Tests.DomainModel.Dsl;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class OrganizationTests
    {
        [Fact]
        public void ShouldBecomeSurnameWithInitials_WhenFullNameContainsALotOfDotsAndSpaces()
        {
            var organization = CreateOrganization("Гендольф.....Антон     Борисович");

            var surnameWithInitials = organization.ShortHeadManagerName;

            Assert.Equal("Гендольф А.Б.", surnameWithInitials);
        }

        [Fact]
        public void ShouldBecomeSurnameWithInitials_WhenOrdinaryFullName()
        {
            var organization = CreateOrganization("Гендольф Антон Борисович");

            var surnameWithInitials = organization.ShortHeadManagerName;

            Assert.Equal("Гендольф А.Б.", surnameWithInitials);
        }

        [Fact]
        public void ShouldBeEmpty_WhenFullNameIsEmpty()
        {
            var organization = CreateOrganization("");

            var surnameWithInitials = organization.ShortHeadManagerName;

            Assert.Equal("", surnameWithInitials);
        }

        private static OrganizationStub CreateOrganization(string headManagerName)
        {
            var country = new Country(0, "", "", 0, "", Currency.Ruble, "");
            var organization =
                new OrganizationStub(0, new Uuid(), "", "", "", "", headManagerName, country, "", "", "");
            return organization;
        }
    }
}