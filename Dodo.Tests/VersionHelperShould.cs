using Dodo.RestaurantBoard.Site.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace Dodo.Tests
{
    public class VersionHelperShould
    {
        [Fact]
        public void ReturnUrlWithVersionToken_WhenAddVersionToken()
        {
            var url = "http://localhost";
            var versionHelper = new TestableVersionHelper("1.1");

            var urlwithToken = versionHelper.AddVersionToken(url);

            Assert.Equal($"{url}?v=1.1", urlwithToken);
        }

    }

    public class TestableVersionHelper : VersionHelper
    {
        private readonly string _version;

        public TestableVersionHelper(string version)
        {
            _version = version;
        }
        public override Version GetExecutingAssemblyVersion()
        {
            return new Version(_version);
        }
    }
}

