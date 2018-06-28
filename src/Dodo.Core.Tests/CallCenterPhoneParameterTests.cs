using Dodo.Core.DomainModel.Departments;
using NUnit.Framework;

namespace Dodo.Core.Tests
{    
    public class CallCenterPhoneParameterTests
    {
        [TestCase("+7(985)123-45-67", "+79851234567")]
        [TestCase(" +7(985) 123-45-67", "+79851234567")]
        public void NumberWithoutMarks_ReplacesCertainCharactersFromNumber(string dirtyPhoneNumber, string expected)
        {
            // Init
            var phone = new CallCenterPhoneParameter {Number = dirtyPhoneNumber};

            // Act
            var actual = phone.NumberWithoutMarks;

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
