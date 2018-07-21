using System;
using System.Collections.Generic;
using System.Text;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.RestaurantBoard.Site.Tests.Dsl.Throws;
using Microsoft.AspNetCore.Mvc;
using NLog.LayoutRenderers.Wrappers;
using Xunit;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Asserts
{
    public class ControllerAssert
    {
        private BoardsController _controller;
           

        public ControllerAssert(BoardsController controller)
        {
            _controller = controller;
        }

        public ThrowHandler<T> Throws<T>() where T : Exception
        {
            return new ThrowHandler<T>();
        }
    }


}
