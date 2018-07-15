using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Microsoft.AspNetCore.Hosting;
using Moq;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class LocalizedContextShould
    {
        [Fact]
        public void CallExistAtLeastOnce_IfContentPathIsNullOrEmpty()
        {
            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            var hostingEnvironmentStub = new Mock<IHostingEnvironment>();
            hostingEnvironmentStub.SetupGet(x => x.WebRootPath).Returns("/usr/local/sbin:/usr/local/");

            LocalizedContext.LocalizedContent(hostingEnvironmentStub.Object, fileServiceMock.Object, "Tracking-Scoreboard-Empty.jpg");

            fileServiceMock.Verify(foo => foo.Exists(It.IsAny<string>()), Times.AtLeastOnce);
        }

        [Fact]
        public void ReturnCorrectUrl_IfContentPathIsCorrectAndContentIsExist()
        {
            var fileServiceStub = new Mock<IFileService>();
            fileServiceStub.Setup(x => x.Exists(It.IsAny<string>())).Returns(true);
            var hostingEnvironmentStub = new Mock<IHostingEnvironment>();
            hostingEnvironmentStub.SetupGet(x => x.WebRootPath).Returns("/usr/local/sbin:/usr/local/");

            var res = LocalizedContext.LocalizedContent(hostingEnvironmentStub.Object, fileServiceStub.Object, "Tracking-Scoreboard-Empty.jpg");
            
            Assert.Equal("//LocalizedResources/ru/Tracking-Scoreboard-Empty.jpg", res);
        }

    }
}