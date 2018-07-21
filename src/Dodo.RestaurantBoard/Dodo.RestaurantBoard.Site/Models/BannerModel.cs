using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dodo.Core.DomainModel.Management;

namespace Dodo.RestaurantBoard.Site.Models
{
    public class BannerModel
    {
        public BannerModel()
        {

        }

        public BannerModel(RestaurantBanner restaurantBanner)
        {
            BannerUrl = restaurantBanner.Url.Replace('\\', '/');
            DisplayTime = restaurantBanner.DisplayTime * 1000;
        }

        public string BannerUrl { get; set; }
        public int DisplayTime { get; set; }


        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj == this) return true;
            return obj is BannerModel banner && Equals(banner);
        }
        protected bool Equals(BannerModel obj)
        {
            return this.BannerUrl == obj.BannerUrl && this.DisplayTime == obj.DisplayTime;
        }
    }
}
