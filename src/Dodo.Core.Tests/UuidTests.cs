using System;
using Dodo.Core.Common;
using NUnit.Framework;

namespace Dodo.Core.Tests
{
    public class UuidTests
    {
        [Test]
        [TestCase("00000000000000000000000000000000")]
        [TestCase("00000000000000000000000000000001")]
        [TestCase("0000000000000000000000000000001a")]
        [TestCase("0000000000000000000000000000001A")]
        public void Constructor_AcceptsCorrectString(string s)
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.DoesNotThrow(() => new Uuid(s));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Constructor_ThrowsOnNullString(string s)
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new Uuid(s));
        }

        [TestCase(1)]
        [TestCase(31)]
        public void Constructor_ThrowsWhenStringIsIncorrectLength(int len)
        {
            var s = new string('0', len);
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentException>(() => new Uuid(s));
        }

        [Test]
        public void Constructor_ThrowsWhenStringContainsInvalidChars()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentException>(() => new Uuid("0000000000000000000000000000000!"));
        }
    }
}