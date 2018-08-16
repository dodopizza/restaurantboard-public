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

            var countrySettings = configManagerStub.Object.GetCountrySettings();
            
            Assert.Equal(null, countrySettings);
        }
    }
}