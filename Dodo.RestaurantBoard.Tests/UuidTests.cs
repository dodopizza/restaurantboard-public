using Dodo.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class UuidTests
    {
        [Fact]
        public void UuidTests_CreateUuidWithEmptyString_Uuid_ThrowArgumentNullExceptin()
        {
            //Arrange
            Action createEmptyUUidAction = () =>  new Uuid("");
            
            //Act Assert
            var exc = Assert.Throws<ArgumentNullException>(()=> createEmptyUUidAction());
        }

        [Fact]
        public void UuidTests_CreateUuidWithNotEqual32LengthString_UUid_ThrowArgumentExeption()
        {
            Action createEmptyUUidAction = () => new Uuid(new String('0',31));

            var exc = Assert.Throws<ArgumentException>(() => createEmptyUUidAction());
        }

        [Fact]
        public void UuidTests_CreateUuidWithUnsuitableCharacters_Uuid_ThrowArgumentExeptionWithSpecificMessage()
        {
            Action createEmptyUUidAction = () => new Uuid($"r{new String('0', 31)}");


            var exc = Assert.Throws<ArgumentException>(() => createEmptyUUidAction());
            Assert.Equal("UUId must have the same characters like guid", exc.Message);
        }

        [Fact]
        public void UuidTests_UseToStringMethod_Uuid_ToStringMethodIsOverrided()
        {
            var uuidStr = new String('0', 32);
            var uuid = new Uuid(uuidStr);

            var toStringResult = uuid.ToString();

            Assert.Equal(uuidStr, toStringResult);
        }
    }
}
