using Dodo.RestaurantBoard.Site.Services;
using System;
using System.Linq;
using Xunit;

namespace Dodo.RestorauntBoard.Tests
{
    public class RestorauntBannerServiceTests
    {
        [Fact]
        public void GetDefaultResult_SholudReturnSingleObject()
        {
            var restorauntBannerService = new RestorauntBannerServiceFake();

            var defaultResult = restorauntBannerService.GetDefaultResult();

            Assert.Single(defaultResult);
        }
    }

    public class RestorauntBannerServiceFake : RestorauntBannerService
    {
        public RestorauntBannerServiceFake() : base(null)
        {
        }

        protected override string GetDefaultBannerUrl()
        {
            return string.Empty;
        }
    }
}
