using Dodo.Core.Common;
using System;
using Xunit;

namespace Dodo.Core.Tests.Common
{
    public class UuidTests
    {
        [Fact]
        public void ShouldNotCreateUUId_WhenCreateFromEmptyString()
        {
            var emptyString = "";

            Action act = () => new Uuid(emptyString);
            
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ShouldNotCreateUUId_WhenCreateFromNull()
        {
            string nullString = null;

            Action act = () => new Uuid(nullString);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ShouldNotCreateUUId_WhenCreateFromStringShorterThan32Symbols()
        {
            var shortString = "123";

            Action act = () => new Uuid(shortString);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ShouldNotCreateUUId_WhenCreateFromStringLongerThan32Symbols()
        {
            var longString = new string(' ', 33);

            Action act = () => new Uuid(longString);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ShouldNotCreateUUId_WhenCreateFromStringIsNotGuid()
        {
            var notGuidString = new string('h', 32);

            Action act = () => new Uuid(notGuidString);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ShouldCreateUUId_WhenCreateFromGuidString()
        {
            var guidString = Guid.NewGuid().ToString().Replace("-", "");

            var uuid = new Uuid(guidString);

            Assert.Equal(uuid.ToString(), guidString);
        }

    }
}
