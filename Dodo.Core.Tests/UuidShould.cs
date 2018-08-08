using System;
using System.Linq;
using System.Text;
using Dodo.Core.Common;
using Xunit;

namespace Dodo.Core.Tests
{
    public class UuidShould
    {
        [Fact]
        public void CorrectConvertToBytesArray()
        {
            var uuidString = "0000AB00000000000000000000000000";
            var uuid = new Uuid(uuidString);

            var expected = new Byte[] { 0, 0, 171, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            var result = uuid.ToByteArray();
            
            Assert.True(expected.SequenceEqual(result));
        }
    }
}