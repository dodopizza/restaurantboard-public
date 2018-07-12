using Dodo.Core.DomainModel.Departments;
using NUnit.Framework;

namespace Dodo.Core.Tests
{    
    public class CallCenterPhoneParameterShould
    {
        [Test]
        public void HaveNormalizedPhoneNumberAsNumberWithoutMarks_WhenNumberContainsMarks()
        {
            var phone = new CallCenterPhoneParameter {Number = "+7(985) 123-45-67"};

            var actual = phone.NumberWithoutMarks;

            Assert.AreEqual("+79851234567", actual);
        }
    }
}
