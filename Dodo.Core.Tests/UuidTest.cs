using Dodo.Core.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.Core.Tests
{
    [TestClass]
    public class UuidTest
    {
        [TestMethod]
        public void Uuid_Null()
        {
            string input = null;
            Assert.ThrowsException<ArgumentNullException>(() => { var sut = new Uuid(input); });
        }
        
    }
}
