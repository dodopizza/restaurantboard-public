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
            var urlWithVersion = VersionHelper.AddVersionToken(url);

            // Assert
            Assert.True(urlWithVersion.Contains("?"));
        }

        [Test]
        public void AddVersionToken_SavesBaseUrl()
        {
            // Arrange
            var url = "foo";

            // Act
            var urlWithVersion = VersionHelper.AddVersionToken(url);

            // Assert
            Assert.True(urlWithVersion.StartsWith(url));
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
