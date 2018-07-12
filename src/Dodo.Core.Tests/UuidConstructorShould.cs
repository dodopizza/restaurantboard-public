using System;
using Dodo.Core.Common;
using NUnit.Framework;

namespace Dodo.Core.Tests
{
    public class UuidConstructorShould
    {
        [Test]
        public void NotThrowException_WhenArgumentContainsHexDigitsOnly()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.DoesNotThrow(() => new Uuid("00000000000123456789abcdefABCDEF"));
        }

        [Test]
        public void ThrowException_OnNullString()
        {
            const string nullString = (string)null;

            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new Uuid(nullString));
        }
        
        [Test]
        public void ThrowException_OnEmptyString()
        {
            const string emptyString = "";
            
            // ReSharper disable once ObjectCreationAsStatement            
            Assert.Throws<ArgumentNullException>(() => new Uuid(emptyString));
        }

        [Test]
        public void ThrowException_WhenStringIsLessThen32Chars()
        {
            var shortString = new string('0', 31);
            
            // ReSharper disable once ObjectCreationAsStatement            
            Assert.Throws<ArgumentException>(() => new Uuid(shortString));
        }
        
        [Test]
        public void ThrowException_WhenStringIsLargerThen32Chars()
        {
            var longString = new string('0', 33);
            
            // ReSharper disable once ObjectCreationAsStatement            
            Assert.Throws<ArgumentException>(() => new Uuid(longString));
        }

        [Test]
        public void ThrowException_WhenStringContainsInvalidChars()
        {
            const string stringWithInvalidChars = "0000000000000000000000000000000!"; 

            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentException>(() => new Uuid(stringWithInvalidChars));
        }
    }
}