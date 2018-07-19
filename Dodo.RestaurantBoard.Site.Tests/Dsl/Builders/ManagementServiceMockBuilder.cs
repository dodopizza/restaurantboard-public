using System;
using Dodo.Core.DomainModel.Management;
using Dodo.Core.DomainModel.Products;
using Dodo.Core.Services;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Builders
{
    public class ManagementServiceMockBuilder
    {
        private Mock<IManagementService> _service;

        public ManagementServiceMockBuilder()
        {
            _service = new Mock<IManagementService>();
        }

        public ManagementServiceMockBuilder WithAvailableBanner(RestaurantBanner restaurantBanner = null)
        {
            var banner = restaurantBanner ?? new RestaurantBanner()
            {
                MenuSpecializationTypes = new[]
                {
                    MenuSpecializationType.European
                },
                Url = "ya.ru",
                DisplayTime = 15
            };

            var banners = new[]
            {
                banner
            };

            _service
                .Setup(x => x.GetAvailableBanners(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .Returns(() => banners);
            return this;
        }

        public IManagementService Please()
        {
            return _service.Object;
        }
    }
}