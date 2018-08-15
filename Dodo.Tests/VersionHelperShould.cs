using Dodo.RestaurantBoard.Site.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Xunit;

namespace Dodo.Tests
{
    public class VersionHelperShould
    {
        [Fact]
        public void ReturnUrlWithVersionToken_OnRequest()
        {
            var url = "http://localhost";
            var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);

            var urlwithToken = new VersionHelper().AddVersionTokenNew(url);

            Assert.Equal($"{url}?v={assemblyVersion}", urlwithToken);
        }
    }
}
