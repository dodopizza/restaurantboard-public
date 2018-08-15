using System;
using System.Reflection;
using Dodo.RestaurantBoard.Site.Common.Helpers;
using NUnit.Framework;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class VersionHelperTest
    {
        [Test]
        public void AddVersionToken_AddsQuestionMark()
        {
            // Arrange
            var url = "foo";
            
            // Act
            var urlWithVersion = new VersionHelper().AddVersionToken(url);

            // Assert
            Assert.True(urlWithVersion.Contains("?"));
        }

        [Test]
        public void AddVersionToken_SavesBaseUrl()
        {
            // Arrange
            var url = "foo";

            // Act
            var urlWithVersion = new VersionHelper().AddVersionToken(url);

            // Assert
            Assert.True(urlWithVersion.StartsWith(url));
        }

        [Test]
        public void AddVersionToken_AddsVersion()
        {
            // Arrange
            var url = "foo";
            var version = "2.0";
            var helper = new TestableVersionHelper(version);

            // Act
            var urlWithVersion = helper.AddVersionToken(url);

            // Assert
            Assert.True(urlWithVersion.Contains(version));
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
