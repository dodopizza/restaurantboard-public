using NUnit.Framework;
using Dodo.Core.UnitTests.DSL;

namespace Dodo.Core.UnitTests.DomainModel.Departments
{
    [TestFixture]
    public class CallCenterPhoneTests
    {
        private CallCenterPhoneParameterBuilder _builder;

        [SetUp]
        public void Setup()
        {
            _builder = new CallCenterPhoneParameterBuilder();
        }

        [Test]
        [TestCase("7(999)999-99-99")]
        [TestCase("7(999)999-99 99")]
        [TestCase("7 999 999-99-99")]
        [TestCase("7  999  999 99 99")]
        [TestCase("79999999999")]
        public void When_pass_number_with_marks_returns_replaced_string_with_only_digits(string phoneNumber)
        {
            var phoneParameter = _builder
                .WithNumber(phoneNumber)
                .Build();

            Assert.AreEqual("79999999999", phoneParameter.NumberWithoutMarks);
        }

        [Test]
        public void When_pass_number_with_plus_sign_keep_it_in_result()
        {
            string phoneNumber = "+79999999999";

            var phoneParameter = _builder
                .WithNumber(phoneNumber)
                .Build();

            Assert.AreEqual("+79999999999", phoneParameter.NumberWithoutMarks);
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void When_pass_null_or_empty_returns_the_same_string(string phoneNumber)
        {
            var phoneParameter = _builder
                .WithNumber(phoneNumber)
                .Build();

            Assert.AreEqual(phoneNumber, phoneParameter.NumberWithoutMarks);
        }
        
        
        [Test]
        public void When_pass_icon_site_path_returns_the_same_string()
        {
            string iconSitePathParameter = "http://we/are/the/best/team";
            string host = "localhost:5000";

            var iconSitePath = _builder
                .WithIconSitePath(iconSitePathParameter)
                .Build();

            Assert.AreEqual("http://we/are/the/best/team", iconSitePath.GetIconUrl(host));
        }
        
        [Test]
        [TestCase("we/are/the/best/team\\")]
        [TestCase("we/are\\the/best/team\\")]
        public void When_pass_icon_path_returns_string_with_updated_host_and_end(string iconPathParameter)
        {
            string host = "localhost:5000\\";

            var iconPath = _builder
                .WithIconPath(iconPathParameter)
                .Build();

            Assert.AreEqual("localhost:5000/we/are/the/best/team/", iconPath.GetIconUrl(host));
        }
        
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void When_pass_empty_icon_path_returns_empty_string(string iconPathParameter)
        {
            string host = "localhost:5000\\";

            var iconPath = _builder
                .WithIconPath(iconPathParameter)
                .Build();

            Assert.AreEqual("", iconPath.GetIconUrl(host));
        }
    }
}