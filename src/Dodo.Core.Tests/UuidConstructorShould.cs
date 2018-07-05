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
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new Uuid((string)null));
        }
        
        [Test]
        public void ThrowException_OnEmptyString()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new Uuid(""));
        }

        [Test]
        public void ThrowException_WhenStringIsTooShort()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentException>(() => new Uuid(new string('0', 31)));
        }
        
        [Test]
        public void ThrowException_WhenStringIsTooLong()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentException>(() => new Uuid(new string('0', 33)));
        }

        [Test]
        public void ThrowException_WhenStringContainsInvalidChars()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentException>(() => new Uuid("0000000000000000000000000000000!"));
        }
    }
}