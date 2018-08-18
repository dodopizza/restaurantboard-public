using Dodo.RestaurantBoard.Site.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.Tests
{
    public class LocalizedContextShould
    {
        [Fact]
        public void ReturnLocalPath_WhenSearchLocally()
        {
            var localizedContext = new TestableLocalizedContext(false);
            string localPath;

            localizedContext.SearchInLocalPath("banner.jpg", out localPath);

            Assert.Equal($@"C:\Localizations\banner.jpg", localPath);
        }

        public class TestableLocalizedContext: LocalizedContext
        {
            private readonly bool shouldFind;

            public TestableLocalizedContext(bool shouldFind)
            {
                this.shouldFind = shouldFind;
            }
            public override bool SearchInFolder(string folder)
            {
                return shouldFind;
            }
        }
    }
}
