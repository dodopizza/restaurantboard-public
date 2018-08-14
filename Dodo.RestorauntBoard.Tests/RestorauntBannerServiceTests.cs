using Dodo.RestaurantBoard.Site.Services;
using System.Collections.Generic;
using System.Linq;
using Dodo.Core.DomainModel.Management;
using Xunit;

namespace Dodo.RestorauntBoard.Tests
{
    public class RestorauntBannerServiceTests
    {
        private const int DepartmentId = 0;
        private const int CountryId = 0;
        private const int UnitId = 0;

        [Fact]
        public void GetDefaultResult_SholudReturnSingleObject()
        {
            var restorauntBannerService = new RestorauntBannerServiceFake();

            var defaultResult = restorauntBannerService.GetDefaultResult();

            Assert.Single(defaultResult);
        }

        [Fact]
        public void GetAvailableBanners_IfAvailableBannersEmpty_ShouldReturnDefaultBanner()
        {
            var restorauntBannerService = new RestorauntBannerServiceFake();

            var availableBanners = restorauntBannerService.GetBanners(
               DepartmentId,
               CountryId,
               UnitId);

            Assert.Single(availableBanners);
        }
    }

    public class RestorauntBannerServiceFake : RestorauntBannerService
    {
        public RestorauntBannerServiceFake() : base(
            null, 
            null, 
            null)
        {
        }

        protected override string GetDefaultBannerUrl()
        {
            return string.Empty;
        }

        protected override IEnumerable<RestaurantBanner> GetAvailableBanners(int countryId, int departmentId, int unitId)
        {
            return Enumerable.Empty<RestaurantBanner>();
        }
    }
}
