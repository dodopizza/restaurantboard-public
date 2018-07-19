using System;
using System.Collections.Generic;
using System.Text;
using Dodo.RestaurantBoard.Site.Controllers;
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

        public T ExecuteOrdersReadinessToStationary<T>() where T : Exception
        {
            return Assert.Throws<T>(() => _controller.OrdersReadinessToStationary(200));
        }
    }

    public static class ExceptionExtentions
    {
        public static void CousesErrorWithParamName(this ArgumentException ex, string paraName)
        {
            Assert.Equal(paraName, ex.ParamName);
        }

        public static void CousesErrorWithMessage(this NullReferenceException ex, string message)
        {
            Assert.Equal(message, ex.Message);
        }
    }
}
