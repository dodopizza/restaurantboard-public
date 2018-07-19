using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dodo.Core.DomainModel.Management;
using Dodo.RestaurantBoard.Site.Models;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Asserts
{
    public class BannersAssert
    {
        private readonly IEnumerable<BannerModel> _banners;

        public BannersAssert(IEnumerable<BannerModel> banners)
        {
            _banners = banners;
        }

        public BannerAssert OnlyOne()
        {
            Assert.Single(_banners);
            return new BannerAssert(_banners.Single());
        }
    }

    public class BannerAssert
    {
        private readonly BannerModel _model;

        public BannerAssert(BannerModel model)
        {
            _model = model;
        }

        public void SameAs(RestaurantBanner banner)
        {
            Assert.Equal(_model.BannerUrl, banner.Url);
            Assert.Equal(_model.DisplayTime, banner.DisplayTime*1000);
        }
    }
}
