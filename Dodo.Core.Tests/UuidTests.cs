using System;
using Dodo.Core.Common;
using Xunit;

namespace Dodo.Core.Tests
{
    public class UuidTests
    {
        [Fact]
        public void ConstructorAcceptingString_IfAcceptsValidValue_ThenCreatesClass()
        {
            var guidStr = "411dee2c7a3111e8adc0fa7ae01bbebc";
            
            var uuid = new Uuid(guidStr);

            Assert.Equal(uuid.ToString(), guidStr);
        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ConstructorAcceptingString_IfAcceptsNullOrEmptyValue_ThenThrowsArgumentNullException(string value)
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new Uuid(value));
            
            Assert.Equal(ex.ParamName, "uuid");
        }
        
        [Theory]
        [InlineData("411dee2c7a3111e8adc0fa7ae01bbebcskdjhgf")] // Too long value
        [InlineData("411dee2c7a3111")] // Too short
        public void ConstructorAcceptingString_IfAcceptsTooLongValue_ThenThrowsArgumentException(string value)
        {
            var ex = Assert.Throws<ArgumentException>(() => new Uuid(value));
            
            Assert.Equal(ex.Message, "The length of the String for UUID must be exactly 32 chars.\nParameter name: uuid");
            Assert.Equal(ex.ParamName, "uuid");
        }
        
        [Fact]
        public void ConstructorAcceptingString_IfAcceptsNotGuidValue_ThenThrowsArgumentException()
        {
            var notGuidStr = "411dee2c7a3111e8adc0fa7ae01bbeb!";

            var ex = Assert.Throws<ArgumentException>(() => new Uuid(notGuidStr));
            
            Assert.Equal(ex.Message, "UUId must have the same characters like guid");
            Assert.Equal(ex.ParamName, null);
        }
    }
}