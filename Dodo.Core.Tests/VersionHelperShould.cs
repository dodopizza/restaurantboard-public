using Dodo.RestaurantBoard.Site.Common.Helpers;
using Xunit;

namespace Dodo.Core.Tests
{
    public class VersionHelperShould
    {
        [Fact]
        void ReturnVersionNumberOne()
        {
            var versionHelper = new VersionHelper();
            
            Assert.Equal("v=1.0", versionHelper.GetVersionToken());
        }
    }
}