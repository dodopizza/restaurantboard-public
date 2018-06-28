using Dodo.Core.DomainModel.Departments;
using Microsoft.VisualStudio.TestPlatform.Common.DataCollection;

namespace Dodo.Core.UnitTests.DSL
{
    public class CallCenterPhoneParameterBuilder
    {
        private string _number;
        private string _iconPath;
        private string _iconSitePath;

        public CallCenterPhoneParameterBuilder WithNumber(string number)
        {
            _number = number;
            return this;
        }

        public CallCenterPhoneParameterBuilder WithIconPath(string iconPath)
        {
            _iconPath = iconPath;
            return this;
        }

        public CallCenterPhoneParameterBuilder WithIconSitePath(string iconSitePath)
        {
            _iconSitePath = iconSitePath;
            return this;
        }

        public CallCenterPhoneParameter Build()
        {
            return new CallCenterPhoneParameter
            {
                Number = _number,
                IconPath = _iconPath,
                IconSitePath = _iconSitePath,
            };
        }
    }
}