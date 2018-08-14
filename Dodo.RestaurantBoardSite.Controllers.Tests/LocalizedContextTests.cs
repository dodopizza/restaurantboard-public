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

            var localizedPathWithCulture = LocalizedContext.GetLocalizedPathWithCulture(
                webRootPath: "example.com", contentPath: "content", culture: cultureInfo);

            Assert.Equal("example.com/LocalizedResources\\en\\content", localizedPathWithCulture);

        }
    }
}