using System;
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
        public void ShoudAddSpaceAfterEveryDot_AddSpaceAfterDot()
        {
            var organization = Create.Organization.Please();
            var names = new []{"QQQ", "W.W.W", "EEE."};
            
            organization.AddSpaceAfterDot(names);

            AssertCollections(new[] {"QQQ", "W. W. W", "EEE. "}, names);
        }

        private void AssertCollections(string[] expected, string[] actual)
        {
            Assert.Equal(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                Assert.Equal(expected[i], actual[i]);
            }
        }
    }
}