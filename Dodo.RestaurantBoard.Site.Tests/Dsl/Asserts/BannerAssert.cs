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

        public void Contains(BannerModel bannerModel)
        {
            Assert.Contains(bannerModel, _banners);
        }
    }
}
