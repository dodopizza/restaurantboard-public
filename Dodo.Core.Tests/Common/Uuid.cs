using Dodo.Core.Common;
using System;
using Xunit;
using UUId = Dodo.Core.Common.Uuid;

namespace Dodo.Core.Tests.Common
{
    public class Uuid
    {
        [Fact]
        public void ShouldNotCreate_FromEmptyString()
        {
            var emptyString = "";

            Action act = () => new UUId(emptyString);
            
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ShouldNotCreate_FromNull()
        {
            string nullString = null;

            Action act = () => new UUId(nullString);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ShouldNotCreate_FromStringShorterThan32Symbols()
        {
            var shortString = new string(' ', 31);

            Action act = () => new UUId(shortString);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ShouldNotCreate_FromStringLongerThan32Symbols()
        {
            var longString = new string(' ', 33);

            Action act = () => new UUId(longString);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ShouldNotCreate_FromStringIsNotGuid()
        {
            var notGuidString = new string('h', 32);

            Action act = () => new UUId(notGuidString);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ShouldCreate_FromGuidString()
        {
            var guidString = "0641a6a331294a9ab054461116ce6560";

            var uuid = new UUId(guidString);

            Assert.Equal(uuid.ToString(), guidString);
        }

    }
}
