using Dodo.Core.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dodo.Core.Tests
{
    [TestClass]
    public class UuidTest
    {
        [TestMethod]
        public void Constructor_TrowsArgumentNullException_WhenInputStringIsNull()
        {
            // Arrange
            string nullString = null;

            // Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => { new Uuid(nullString); });
        }        
    }
}
