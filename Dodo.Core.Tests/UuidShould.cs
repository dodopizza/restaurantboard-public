using System;
using Dodo.Core.Common;
using Xunit;

namespace Dodo.Core.Tests
{
    public class UuidShould
    {
        [Fact]
        public void CreateUuidInstance_IfValidStringIsPassedIntoConstructor()
        {
            const string validString = "411dee2c7a3111e8adc0fa7ae01bbebc";
            
            var uuid = new Uuid(validString);

            Assert.NotNull(uuid);
        }

        [Fact]
        public void ThrowArgumentNullException_IfNullIsPassedIntoConstructor()
        {
            const string nullString = null;

            var ex = Assert.Throws<ArgumentNullException>(() => new Uuid(nullString));
            
            Assert.Equal("uuid", ex.ParamName);
        }
        
        [Fact]
        public void ThrowArgumentNullException_IfEmptyStringIsPassedIntoConstructor()
        {
            const string emptyString = "";

            var ex = Assert.Throws<ArgumentNullException>(() => new Uuid(emptyString));
            
            Assert.Equal("uuid", ex.ParamName);
        }
        
        [Fact]
        public void ThrowArgumentException_IfStringLongerThan32CharactersIsPassedIntoConstructor()
        {
            var tooLongString = new string('0', 33);

            var ex = Assert.Throws<ArgumentException>(() => new Uuid(tooLongString));
            
            Assert.Equal("The length of the String for UUID must be exactly 32 chars.\nParameter name: uuid", ex.Message);
            Assert.Equal("uuid", ex.ParamName);
        }
        
        [Fact]
        public void ThrowArgumentException_IfStringShorterThan32CharactersIsPassedIntoConstructor()
        {
            var tooShortString = new string('0', 31);

            var ex = Assert.Throws<ArgumentException>(() => new Uuid(tooShortString));
            
            Assert.Equal("The length of the String for UUID must be exactly 32 chars.\nParameter name: uuid", ex.Message);
            Assert.Equal("uuid", ex.ParamName);
        }
        
        [Fact]
        public void ThrowArgumentException_IfStringWithNonGuidCharactersIsPassedIntoConstructor()
        {
            var nonGuidString = new string('!', 32);

            var ex = Assert.Throws<ArgumentException>(() => new Uuid(nonGuidString));
            
            Assert.Equal("UUId must have the same characters like guid", ex.Message);
            Assert.Null(ex.ParamName);
        }
    }
}