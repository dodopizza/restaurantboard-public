using System;
using Xunit;
using Dodo.Core.DomainModel.Clients;

namespace Dodo.Core.UnitTests
{
    public class ClientIconShould
    {
        [Fact]
        public void CreateFullPath_ByKnownHostAndFileName()
        {
            var clientIcon = new ClientIcon(1, "icon.png");
            var url = clientIcon.GetUrl("https://testFileStorageHost/");
            
            Assert.Equal("https://testFileStorageHost/icon.png", url);
        }
    }
}