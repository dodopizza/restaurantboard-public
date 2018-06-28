using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.Tests.DomainModel.Dsl;
using Xunit;

namespace Dodo.Core.Tests.DomainModel
{
    public class OrganizationTests
    {
        [Theory]
        [InlineData("Гендольф Антон Борисович", "Гендольф А.Б.")]
        [InlineData("Гендольф.Антон     Борисович", "Гендольф А.Б.")]
        [InlineData("", "")]
        [InlineData("    ", "")]
        [InlineData("Гендольф", "Гендольф ")]
        [InlineData(" .. .. ...", "")]
        public void ShortHeadManagerName_IfFio_SurnameWithInitials(string headManagerName, string expectedShortName)
        {
            var country = new Country(0, "", "", 0, "", Currency.Ruble, "");
            var organization = new OrganizationStub(0, new Uuid(), "", "", "", "", headManagerName, country, "", "", "");

            var actual = organization.ShortHeadManagerName;

            Assert.Equal(expectedShortName, actual);
        }


    }
}