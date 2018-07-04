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
        public void WhenNumberContainsOnlyDigits_ThenNumberWithoutMarksContainsOnlyDigits()
        {
            var phoneParameter = _builder
                .WithNumber("79999999999")
                .Build();
            
            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;

            Assert.AreEqual("79999999999", numberWithoutMarks);
        }
        
        [Test]
        public void WhenNumberContainsDigitsAndDashes_ThenNumberWithoutMarksContainsOnlyDigits()
        {
            var phoneParameter = _builder
                .WithNumber("7-999-999-99-99")
                .Build();

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;
            
            Assert.AreEqual("79999999999", numberWithoutMarks);
        }
        
        [Test]
        public void WhenNumberContainsDigitsAndSpaces_ThenNumberWithoutMarksContainsOnlyDigits()
        {
            var phoneParameter = _builder
                .WithNumber("7 999 999 99 99")
                .Build();

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;
            
            Assert.AreEqual("79999999999", numberWithoutMarks);
        }
        
        [Test]
        public void WhenNumberContainsDigitsAndBraces_ThenNumberWithoutMarksContainsOnlyDigits()
        {
            var phoneParameter = _builder
                .WithNumber("7(999)9999999")
                .Build();

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;
            
            Assert.AreEqual("79999999999", numberWithoutMarks);
        }
        
        [Test]
        public void WhenNumberContainsDigitsSpacesDashesBraces_ThenNumberWithoutMarksContainsOnlyDigits()
        {
            var phoneParameter = _builder
                .WithNumber("7 (999) 999-99-99")
                .Build();

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;
            
            Assert.AreEqual("79999999999", numberWithoutMarks);
        }

        [Test]
        public void WhenNumberContainsDigitsAndPlusSign_ThenNumberWithoutMarksKeepsPlusSignAndDigits()
        {
            var phoneParameter = _builder
                .WithNumber("+79999999999")
                .Build();

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;

            Assert.AreEqual("+79999999999", numberWithoutMarks);
        }

        [Test]
        public void WhenNumberIsEmpty_ThenNumberWithoutMarksIsEmpty()
        {
            var phoneParameter = _builder
                .WithNumber("")
                .Build();

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;

            Assert.AreEqual("", numberWithoutMarks);
        }
        
        [Test]
        public void WhenNumberIsNull_ThenNumberWithoutMarksIsNull()
        {
            var phoneParameter = _builder
                .WithNumber(null)
                .Build();

            var numberWithoutMarks = phoneParameter.NumberWithoutMarks;
            
            Assert.AreEqual(null, numberWithoutMarks);
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
        public void When_pass_icon_path_returns_combined_url_string(string iconPathParameter)
        {
            string host = "https://dodopizza.ru/";

            var iconPath = _builder
                .WithIconPath(iconPathParameter)
                .Build();

            Assert.AreEqual("https://dodopizza.ru/we/are/the/best/team/", iconPath.GetIconUrl(host));
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