using System;
using System.Globalization;
using System.Threading;
using Dodo.RestaurantBoard.Site.Controllers;
using Xunit;

namespace Dodo.RestaurantBoardSite.Controllers.Tests
{
    public class LocalizedContextTests
    {
        [Fact]
        public void GetLocalizedPathWithCulture_ShouldReturnValidCombinedPath_DependsOnThreadCulture()
        {
            var cultureInfo = new CultureInfo("en-US");

            var localizedPathWithCulture = new LocalizedContext().GetLocalizedPathWithCulture("example.com", "content", cultureInfo);

            Assert.Equal("example.com/LocalizedResources\\en\\content", localizedPathWithCulture);
        }
        
        [Fact]
        public void GetLocalizedPathWithoutCulture_ShouldReturnValidCombinedPath()
        {
            var localizedPath = LocalizedContext.GetLocalizedPath(
                webRootPath: "example.com", contentPath: "content");

            Assert.Equal("example.com/LocalizedResources\\content", localizedPath);
        }
    }
}