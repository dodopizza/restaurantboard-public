using System;
using System.Reflection;
using Dodo.RestaurantBoard.Site.Common.Helpers;
using NUnit.Framework;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class VersionHelperTest
    {
        [Test]
        public void AddVersionToken()
        {
            // Arrange
            var helper = new TestableVersionHelper("2.0");

            // Act
            var urlWithVersion = helper.AddVersionToken("foo");

            // Assert
            Assert.AreEqual("foo?v=2.0", urlWithVersion);
        }

        [Test]
        public void AddVersionToken_KeepsArguments()
        {
            // Arrange
            var helper = new TestableVersionHelper("2.0");

            // Act
            var urlWithVersion = helper.AddVersionToken("foo?arg=val");

            // Assert
            Assert.AreEqual("foo?v=2.0&arg=val", urlWithVersion);
        }

        class TestableVersionHelper : VersionHelper
        {
            private readonly string _version;

            public TestableVersionHelper(string version)
            {
                _version = version;
            }

            protected override Version GetVersion()
            {
                return new Version(_version);
            }
        }
    }
}
