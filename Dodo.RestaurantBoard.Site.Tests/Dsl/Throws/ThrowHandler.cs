using System;
using System.Collections.Generic;
using System.Text;
using NLog.Filters;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Throws
{
    public class ThrowHandler<T> where T: Exception
    {
        public void On(Action action)  
        {
            Assert.Throws<T>(action);
        }
    }
}
