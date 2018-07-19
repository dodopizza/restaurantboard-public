using Dodo.Core.DomainModel.Departments;

namespace Tests.Dsl
{
    public class CallCenterPhoneParametersBuilder
    {
        private string _number;
        private string _iconPath;
        private string _iconSitePath;

        public CallCenterPhoneParametersBuilder WithIconPath(string iconPath)
        {
            _iconPath = iconPath;
            return this;
        }

        public CallCenterPhoneParametersBuilder WithSiteIconPath(string iconSitePath)
        {
            _iconSitePath = iconSitePath;
            return this;
        }

        public CallCenterPhoneParametersBuilder WithNumber(string number)
        {
            _number = number;
            return this;
        }

        public CallCenterPhoneParameter Please()
        {
            return new CallCenterPhoneParameter
            {
                Number = _number,
                IconPath = _iconPath,
                IconSitePath = _iconSitePath
            };
        }
    }
}