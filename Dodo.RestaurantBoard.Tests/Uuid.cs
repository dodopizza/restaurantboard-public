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
            Action createEmptyUUidAction = () =>  new Core.Common.Uuid("");
            
            var exc = Assert.Throws<ArgumentNullException>(()=> createEmptyUUidAction());
        }

        [Fact]
        public void ShouldThrowArgumentExeption_WhenCreateFromStringNot32Symbols()
        {
            Action createEmptyUUidAction = () => new Core.Common.Uuid(new String('0', 31));

            var exc = Assert.Throws<ArgumentException>(() => createEmptyUUidAction());
        }

        [Fact]
        public void ShouldThrowArgumentExeptionWithSpecificMessage_WhenStringContainLetters()
        {
            Action createEmptyUUidAction = () => new Core.Common.Uuid($"r{new String('0', 31)}");

            var exc = Assert.Throws<ArgumentException>(() => createEmptyUUidAction());

            Assert.Equal("UUId must have the same characters like guid", exc.Message);
        }

        [Fact]
        public void ShouldReturnUuid_WhenInvokeToStringMethod()
        {
            var uuidString = new String('0', 32);
            var uuid = new Core.Common.Uuid(uuidString);

            var toStringResult = uuid.ToString();

            Assert.Equal(uuidString, toStringResult);
        }
    }
}
