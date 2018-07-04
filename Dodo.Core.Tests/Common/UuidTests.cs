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
            var uuidFrom = "";

            Action act = () => new Uuid(uuidFrom);
            
            Assert.Throws<ArgumentNullException>(act);
        }
        
        [Theory]
        [InlineData(null)]
        public void Uuid_ThrowArgumentNullException_IfEmptyOrNullString(string uuid)
        {
            Assert.Throws<ArgumentNullException>(() => new Uuid(uuid));
        }

        [Theory]
        [InlineData("123")]
        [InlineData("1234567890123456789012345678901234567890")]
        public void Uuid_ThrowArgumentException_IfIncorrectLength(string uuid)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Uuid(uuid));
            Assert.Equal("The length of the String for UUID must be exactly 32 chars.\r\nParameter name: uuid", exception.Message);
        }

        [Theory]
        [InlineData("12345678901234z67890123456789012")]
        public void Uuid_ThrowArgumentException_IfNotGuid(string uuid)
        {
            var exception = Assert.Throws<ArgumentException>(() => new Uuid(uuid));
            Assert.Equal("UUId must have the same characters like guid", exception.Message);
        }

        [Fact]
        public void Uuid_ShouldCreate_IfGuid()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");

            var uuid = new Uuid(guid);

            Assert.Equal(uuid.ToString(), guid);
        }

    }
}
