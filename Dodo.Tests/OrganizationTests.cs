using System;
using Dodo.Core.Common.Enums;
using Dodo.Core.DomainModel.Management.Organizations;
using Dodo.Tests.DSL;
using Xunit;

namespace Dodo.Tests
{
    public class OrganizationTests
    {
        [Fact]
        public void ShoudCutAllElementsFromSecond_WhenGetInititals()
        {
            var organization = Create.Organization.Please();
            var names = new[] { "QQQ", "WWW", "EEE" };
            
            organization.GetInititals(names);

            Assert.Equal(new[] { "QQQ", "W.", "E." }, names);
        }
        
        [Fact]
        public void ShoudAddSpaceAfterLastDot_WhenAddSpaceAfterDot()
        {
            var organization = Create.Organization.Please();
            var names = new[] { "WWW." };
            
            organization.AddSpaceAfterDot(names);

            Assert.Equal(new[] { "WWW. " }, names);
        }

        [Fact]
        public void ShoudAddSpaceAfterCenterDot_WhenAddSpaceAfterDot()
        {
            var organization = Create.Organization.Please();
            var names = new[] { "W.W.W" };

            organization.AddSpaceAfterDot(names);

            Assert.Equal(new[] { "W. W. W" }, names);
        }

        [Fact]
        public void ShoudNotAddSpaceWithoutDot_WhenAddSpaceAfterDot()
        {
            var organization = Create.Organization.Please();
            var names = new[] { "WWW" };

            organization.AddSpaceAfterDot(names);

            Assert.Equal(new[] { "WWW" }, names);
        }

        [Fact]
        public void ShoudReturnEmpty_WhenGetAvailableTypesForZh()
        {
            var organization = Create.Organization.WithCountryCode(CountryCode.Zh).Please();

            var types = organization.GetAvailableTypes();

            Assert.Equal(new OrganizationType[0], types);
        }

        [Fact]
        public void ShoudReturnOrganizationTypesForRu_WhenGetAvailableTypesForRu()
        {
            var organization = Create.Organization.WithCountryCode(CountryCode.Ru).Please();
            var expected = new[]
                {OrganizationType.Rus_IP, OrganizationType.Rus_OAO, OrganizationType.Rus_OOO, OrganizationType.Rus_ZAO};

            var types = organization.GetAvailableTypes();

            Assert.Equal(expected, types);
        }

       
    }
}