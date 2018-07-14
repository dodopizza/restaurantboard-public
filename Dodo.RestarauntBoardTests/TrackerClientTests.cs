using System;
using System.Collections.Generic;
using System.Text;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.RestaurantBoard.Domain.Stores;
using Dodo.Tracker.Contracts;
using Moq;
using Xunit;

namespace Dodo.RestarauntBoardTests
{
    public class TrackerClientTests
    {

        // Behaviour
        [Fact]
        public void AddOrdersShoudInvokeOnce_WhenPlaceOrder()
        {
            var productOrder = new ProductionOrder();
            var orderStoreMock = new Mock<IOrdersStore>();
            var orderStore = orderStoreMock.Object;
            var trackerClient = new TrackerClient(orderStore);

            trackerClient.PlaceOrder(productOrder);

            orderStoreMock.Verify(m=>m.AddOrder(productOrder),Times.Once);
        }

        // Behaviour
        [Fact]
        public void GetOrdersShoudInvokeOnce_WhenGetOrders()
        {
            var orderStoreMock = new Mock<IOrdersStore>();
            var orderStore = orderStoreMock.Object;
            var trackerClient = new TrackerClient(orderStore);

            trackerClient.GetOrders();

            orderStoreMock.Verify(m => m.GetOrders(), Times.Once);
        }

        // Behaviour
        [Fact]
        public void ShoudNotInvokeGetOrders_WhenPlaceOrder()
        {
            var orderStoreMock = new Mock<IOrdersStore>();
            var productOrder = new ProductionOrder();
            var orderStore = orderStoreMock.Object;
            var trackerClient = new TrackerClient(orderStore);

            trackerClient.PlaceOrder(productOrder);

            orderStoreMock.Verify(m => m.GetOrders(), Times.Never);
        }

        // State
        [Fact]
        public void ShouldNotReturnAnyOrder_WhenGetOrdersWithNoOrders()
        {
            var orderStoreMock = new Mock<IOrdersStore>();
            orderStoreMock.Setup(o => o.GetOrders()).Returns(new List<IProductionOrder>());
            var orderStore = orderStoreMock.Object;
            var trackerClient = new TrackerClient(orderStore);

            var orders = trackerClient.GetOrders();

            Assert.Empty(orders);
        }

    }
}
