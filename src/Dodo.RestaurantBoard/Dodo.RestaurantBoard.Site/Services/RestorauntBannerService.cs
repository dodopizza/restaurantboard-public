using Dodo.RestaurantBoard.Site.Controllers;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dodo.RestaurantBoard.Site.Services
{
    public class RestorauntBannerService
    {
        IHostingEnvironment _hostingEnvironment;

        public RestorauntBannerService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IEnumerable<object> GetDefaultResult()
        {
            return new[] 
            {
                new
                {
                    BannerUrl = GetDefaultBannerUrl(),
                    DisplayTime = 60000
                }
            };
        }

        protected virtual string GetDefaultBannerUrl()
        {
            return LocalizedContext.LocalizedContent(_hostingEnvironment, "Tracking-Scoreboard-Empty.jpg");
        }
    }
}
