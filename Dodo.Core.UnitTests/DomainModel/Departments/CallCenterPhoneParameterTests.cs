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
        public void WhenIconPathContainsMixedSlashes_ThenIconUrlIsCombinedFromHostAndIconPathWithNormalSlashes()
        {
            var phoneParameter = _builder
                .WithIconPath("we\\are\\the/best/team\\")
                .Build();

            var iconUrl = phoneParameter.GetIconUrl("https://dodopizza.ru/");

            Assert.AreEqual("https://dodopizza.ru/we/are/the/best/team/", iconUrl);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("localhost")]
        [TestCase("https://dodopizza.ru/")]
        public void WhenIconSiteIsNullAndIconSitePathIsNotNull_ThenIconUrlIsEqualsToIconSitePathForAnyHost(string host)
        {
            var phoneParameter = _builder
                .WithIconPath(null)
                .WithIconSitePath("http://we/are/the/best/team")
                .Build();

            var iconUrl = phoneParameter.GetIconUrl(host);

            Assert.AreEqual("http://we/are/the/best/team", iconUrl);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("localhost")]
        [TestCase("https://dodopizza.ru/")]
        public void WhenIconSiteIsNullAndIconSitePathIsNull_ThenIconUrlIsEmptyStringForAnyHost(string host)
        {
            var phoneParameter = _builder
                .WithIconPath(null)
                .WithIconSitePath(null)
                .Build();

            var iconUrl = phoneParameter.GetIconUrl(host);

            Assert.AreEqual("", iconUrl);
        }
    }
}