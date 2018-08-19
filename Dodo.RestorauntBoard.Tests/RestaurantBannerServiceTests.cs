using Dodo.RestaurantBoard.Site.Services;
using System.Collections.Generic;
using System.Linq;
using Dodo.Core.DomainModel.Management;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class RestaurantBannerServiceTests
    {
        private const int DepartmentId = 0;
        private const int CountryId = 0;
        private const int UnitId = 0;

        [Fact]
        public void GetDefaultResult_SholudReturnSingleObject()
        {
            var restaurantBannerService = new RestaurantBannerServiceFake();

            var actualResult = restaurantBannerService.GetDefaultResult();

            Assert.Single(actualResult);
        }

        [Fact]
        public void GetDefaultResult_SholudBeEqualToDefaultObject()
        {
            var restaurantBannerService = new RestaurantBannerServiceFake();

            var actualResult = restaurantBannerService.GetDefaultResult();

            Assert.Equal(restaurantBannerService.DefaultObject, actualResult.Single());
        }

        [Fact]
        public void GetBanners_IfAvailableBannersEmpty_ShouldReturnDefaultBanner()
        {
            var restaurantBannerService = new RestaurantBannerServiceFake();

            var availableBanners = restaurantBannerService.GetBanners(
               DepartmentId,
               CountryId,
               UnitId);

            Assert.Equal(restaurantBannerService.DefaultResult, availableBanners);
        }
    }

    public class RestaurantBannerServiceFake : RestaurantBannerService
    {
        public IEnumerable<object> DefaultResult { get; private set; }

        public object DefaultObject { get; private set; }

        public RestaurantBannerServiceFake() : base(
            null, 
            null, 
            null)
        {
        }

        public override IEnumerable<object> GetDefaultResult()
        {
            DefaultResult = base.GetDefaultResult();
            return DefaultResult;
        }

        protected override string GetDefaultBannerUrl()
        {
            return string.Empty;
        }

        protected override IEnumerable<RestaurantBanner> GetAvailableBanners(int countryId, int departmentId, int unitId)
        {
            return Enumerable.Empty<RestaurantBanner>();
        }

        protected override object GetDefaultObject()
        {
            DefaultObject = base.GetDefaultObject();
            return DefaultObject;
        }
    }
}
