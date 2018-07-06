using Dodo.Core.Common;
using System;
using Xunit;

namespace Dodo.Core.Tests.Common
{
    public class Uuid
    {
        [Fact]
        public void ShouldNotCreate_WhenCreateFromEmptyString()
        {
            var emptyString = "";

            Action act = () => new Core.Common.Uuid(emptyString);
            
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ShouldNotCreate_WhenCreateFromNull()
        {
            string nullString = null;

            Action act = () => new Core.Common.Uuid(nullString);

            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void ShouldNotCreate_WhenCreateFromStringShorterThan32Symbols()
        {
            var shortString = new string(' ', 31);

            Action act = () => new Core.Common.Uuid(shortString);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ShouldNotCreate_WhenCreateFromStringLongerThan32Symbols()
        {
            var longString = new string(' ', 33);

            Action act = () => new Core.Common.Uuid(longString);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ShouldNotCreate_WhenCreateFromStringIsNotGuid()
        {
            var notGuidString = new string('h', 32);

            Action act = () => new Core.Common.Uuid(notGuidString);

            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void ShouldCreate_WhenCreateFromGuidString()
        {
            var guidString = "0641a6a331294a9ab054461116ce6560";

            var uuid = new Core.Common.Uuid(guidString);

            Assert.Equal(uuid.ToString(), guidString);
        }

    }
}
