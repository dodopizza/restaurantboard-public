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
        public void Constructor_AcceptsCorrectString(string uuidStringRepresentation)
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.DoesNotThrow(() => new Uuid(uuidStringRepresentation));
        }

        [Test]
        public void Constructor_ThrowsOnNullString()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new Uuid((string)null));
        }
    }
}