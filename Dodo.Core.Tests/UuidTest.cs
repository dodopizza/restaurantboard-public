using Dodo.Core.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dodo.Core.Tests
{
    [TestClass]
    public class UuidTest
    {
        [TestMethod]
        public void Ctor_WhenUuIdIsNull_ThenTrowsArgumentNullException()
        {
            // Arrange
            string uuid = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => { new Uuid(uuid); });
        }        
    }
}
