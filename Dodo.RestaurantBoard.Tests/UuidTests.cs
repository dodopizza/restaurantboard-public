using Dodo.Core.Common;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class UuidTests
    {
        string uuidWithNotEqual32Length = new String('0', 31);
        string uuidWithUnsuitablecharacters = $"r{new String('0', 31)}";
        string uuidWithCorrectContent = new String('0', 32);

        [Fact]
        public void CreateUuidWithEmptyString_Uuid_ThrowArgumentNullExceptin()
        {
            //Arrange
            Action createEmptyUUidAction = () =>  new Uuid("");
            
            //Act Assert
            var exc = Assert.Throws<ArgumentNullException>(()=> createEmptyUUidAction());
        }

        [Fact]
        public void CreateUuidWithNotEqual32LengthString_UUid_ThrowArgumentExeption()
        {
            Action createEmptyUUidAction = () => new Uuid(uuidWithNotEqual32Length);

            var exc = Assert.Throws<ArgumentException>(() => createEmptyUUidAction());
        }

        [Fact]
        public void CreateUuidWithUnsuitableCharacters_Uuid_ThrowArgumentExeptionWithSpecificMessage()
        {
            Action createEmptyUUidAction = () => new Uuid(uuidWithUnsuitablecharacters);


            var exc = Assert.Throws<ArgumentException>(() => createEmptyUUidAction());
            Assert.Equal("UUId must have the same characters like guid", exc.Message);
        }

        [Fact]
        public void UseToStringMethod_Uuid_ToStringMethodIsOverrided()
        {
            var uuid = new Uuid(uuidWithCorrectContent);

            var toStringResult = uuid.ToString();

            Assert.Equal(uuidWithCorrectContent, toStringResult);
        }
    }
}
