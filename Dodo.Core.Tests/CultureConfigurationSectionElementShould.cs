using System;
using System.Configuration;
using Dodo.Core.Common;
using Xunit;
using Moq;

namespace Dodo.Core.Tests
{
    public class CultureConfigurationSectionElementShould
    {
        [Fact]
        public void ReturnNull_IfCountrySettingsIsNull()
        {
            var configManagerStub = new Mock<CultureConfigurationSectionElement>();
            configManagerStub.Setup(x => x.GetCountrySettings()).Returns((StartupSettingsCountryConfigSection)null);

            var countrySettings = configManagerStub.Object.GetAvailableCultures();
            
            Assert.Equal(null, countrySettings);
        }
        
        [Fact]
        public void CallOneTimeGetCultureCodes_InGetAvailableCulturesMethod()
        {
            var configManagerMock = new Mock<CultureConfigurationSectionElement>();
            configManagerMock.Setup(x => x.GetCountrySettings()).Returns(new StartupSettingsCountryConfigSection());
            
            configManagerMock.Object.GetAvailableCultures();
            
            configManagerMock.Verify(x => x.GetCultureCodes(It.IsAny<StartupSettingsCountryConfigSection>()), Times.Once);
        }
    }
}