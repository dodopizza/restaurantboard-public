using Dodo.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class UuidTest
    {
        [Fact]
        public void EmptyUuidShouldThrowExeption()
        {
            var exc = Assert.Throws<ArgumentNullException>(()=> new Uuid(""));
        }

        [Fact]
        public void IncorrectUuidLengthThrowExeption()
        {
            var exc = Assert.Throws<ArgumentException>(() => new Uuid("00000000"));
        }

        [Fact]
        public void IncorrectUuidThrowExeption()
        {
            var exc = Assert.Throws<ArgumentException>(() => new Uuid("r0000000000000000000000000000000"));
            Assert.Equal("UUId must have the same characters like guid", exc.Message);
        }

        [Fact]
        public void IsToStringOverrided()
        {
            var uuidStr = "00000000000000000000000000000001";
            var uuid = new Uuid(uuidStr);

            Assert.Equal(uuidStr, uuid.ToString());
        }
    }
}
