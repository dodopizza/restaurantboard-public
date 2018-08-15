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
    }
}
