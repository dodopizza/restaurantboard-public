using System;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Site.Controllers;
using Dodo.Tracker.Contracts;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class OrdersControllerTests
    {
        // State test
        [Test]
        public void Post_ReturnsBadRequestResult_IfOrderIsNotValid()
        {
            var stubTrackerClient = new Mock<ITrackerClient>();
            stubTrackerClient.Setup(x => x.AddOrder(It.IsAny<ProductionOrder>()))
                .Throws(new ArgumentNullException());
            var ordersController = new OrdersController(stubTrackerClient.Object);

            var result = ordersController.Post(new ProductionOrder());

            Assert.IsInstanceOf<BadRequestResult>(result);
        }
    }
}
