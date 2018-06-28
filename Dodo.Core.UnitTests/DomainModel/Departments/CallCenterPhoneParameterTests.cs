using NUnit.Framework;
using Dodo.Core.DomainModel.Departments;
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
    }
}