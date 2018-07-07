using Dodo.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class Uuid
    {

        [Fact]
        public void ShouldThrowArgumentNullException_WhenCreateEmptyString()
        {
            Assert.Throws<ArgumentNullException>(()=> new Core.Common.Uuid(""));
        }

        [Fact]
        public void ShouldThrowArgumentExeption_WhenCreateFromStringNot32Symbols()
        {
            Assert.Throws<ArgumentException>(() => new Core.Common.Uuid(new String('0', 31)));
        }

        [Fact]
        public void ShouldThrowArgumentExeptionWithSpecificMessage_WhenStringContainLetters()
        {
            var exc = Assert.Throws<ArgumentException>(() => new Core.Common.Uuid($"r{new String('0', 31)}"));

            Assert.Equal("UUId must have the same characters like guid", exc.Message);
        }

        [Fact]
        public void ShouldReturnUuid_WhenInvokeToStringMethod()
        {
            var uuid = new Core.Common.Uuid(new String('0', 32));

            Assert.Equal(new String('0', 32), uuid.ToString());
        }
    }
}
