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
            var names = new []{"QQQ", "WWW", "EEE"};
            
            organization.GetInititals(names);

            AssertCollections(new[] {"QQQ", "W.", "E."}, names);
        }
        
        [Fact]
        public void ShoudAddSpaceAfterEveryDot_WhenAddSpaceAfterDot()
        {
            var organization = Create.Organization.Please();
            var names = new []{"QQQ", "W.W.W", "EEE."};
            
            organization.AddSpaceAfterDot(names);

            AssertCollections(new[] {"QQQ", "W. W. W", "EEE. "}, names);
        }
        
        [Fact]
        public void ShoudReturnEmpty_WhenGetAvailableTypesForZh()
        {
            var organization = Create.Organization.WithCountryCode(CountryCode.Zh).Please();

            var types = organization.GetAvailableTypes();

            AssertCollections(new OrganizationType[0], types);
        }

        [Fact]
        public void ShoudReturnOrganizationTypesForRu_WhenGetAvailableTypesForRu()
        {
            var organization = Create.Organization.WithCountryCode(CountryCode.Ru).Please();
            var expected = new[]
                {OrganizationType.Rus_IP, OrganizationType.Rus_OAO, OrganizationType.Rus_OOO, OrganizationType.Rus_ZAO};

            var types = organization.GetAvailableTypes();

            AssertCollections(expected, types);
        }

        private void AssertCollections(string[] expected, string[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }

        private void AssertCollections(OrganizationType[] expected, OrganizationType[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}