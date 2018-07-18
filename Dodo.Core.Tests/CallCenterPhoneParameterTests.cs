using System;
using NUnit.Framework;
using Tests.Dsl;

namespace Tests
{
    public class CallCenterPhoneParameterTests
    {
        [Test]
        public void WhenIconPathNotEmpty_ThenShouldCreateIconUrlWithCorrectSlashes()
        {
            var phone = Create.CallCenterPhoneParameter
                .WithIconPath("icon\\path")
                .Please();

            var iconUrl = phone.GetIconUrl("www.example.com");

            Assert.AreEqual("www.example.com/icon/path", iconUrl);
        }

        [Test]
        public void WhenIconPathIsEmptyAndIconSitePathNotEmpty_ThenShouldUseItAsIconUrl()
        {
            var phone = Create.CallCenterPhoneParameter
                .WithIconPath(null)
                .WithSiteIconPath("www.example-site-icon-path.com/icon/path")
                .Please();

            var iconUrl = phone.GetIconUrl("www.example.com");

            Assert.AreEqual("www.example-site-icon-path.com/icon/path", iconUrl);
        }

        [Test]
        public void WhenIconPathAndIconSitePathAreBothEmpty_ThenIconUrlShouldBeEmptyString()
        {
            var phone = Create.CallCenterPhoneParameter
                .WithIconPath(null)
                .WithSiteIconPath(null)
                .Please();

            var iconUrl = phone.GetIconUrl("www.example.com");

            Assert.IsEmpty(iconUrl);
        }

        [Test]
        public void WhenNumberIsEmpty_ThenNumberWithoutMarksShouldBeEmpty()
        {
            var phone = Create.CallCenterPhoneParameter
                .WithNumber(String.Empty)
                .Please();

            var expectedNumberWithoutMarks = phone.NumberWithoutMarks;

            Assert.IsEmpty(expectedNumberWithoutMarks);
        }

        [Test]
        public void WhenNumberIsNull_ThenNumberWithoutMarksShouldBeNull()
        {
            var phone = Create.CallCenterPhoneParameter
                .WithNumber(null)
                .Please();

            var expectedNumberWithoutMarks = phone.NumberWithoutMarks;

            Assert.IsNull(expectedNumberWithoutMarks);
        }

        [Test]
        [TestCase("8 999 999 99 99")]
        [TestCase("8 999 999-99-99")]
        [TestCase("8 (999) 999 99 99")]
        [TestCase("8 (999) 999-99-99")]
        [TestCase("89999999999")]
        public void WhenNumberContainsDashesSpacesOrBrackets_ThenNumberWithoutMarksShouldContainOnlyDigits(string number)
        {
            var phone = Create.CallCenterPhoneParameter
                .WithNumber(number)
                .Please();

            var expectedNumberWithoutMarks = phone.NumberWithoutMarks;

            Assert.AreEqual("89999999999", expectedNumberWithoutMarks);
        }
    }
}