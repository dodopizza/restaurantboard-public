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
       

        [Fact]
        public void AddOrdersShoudInvokeOnce_WhenPlaceOrder()
        {
            var productOrderDummy = new Mock<IProductionOrder>();
            var orderStoreStub = new Mock<IOrdersStore>();
            var productOrder = productOrderDummy.Object;
            var orderStore = orderStoreStub.Object;
            var trackerClient = new TrackerClient(orderStore);

            trackerClient.PlaceOrder(productOrder);

            orderStoreStub.Verify(m=>m.AddOrder(It.IsAny<IProductionOrder>()),Times.Once);
        }

        [Fact]
        public void GetOrdersShoudInvokeOnce_WhenGetOrders()
        {
            var orderStoreStub = new Mock<IOrdersStore>();
            orderStoreStub.Setup(o => o.GetOrders()).Returns(It.IsAny<List<IProductionOrder>>());
            var orderStore = orderStoreStub.Object;
            var trackerClient = new TrackerClient(orderStore);

            trackerClient.GetOrders();

            orderStoreStub.Verify(m => m.GetOrders(), Times.Once);
        }
        [Fact]
        public void ShoudNotInvokeGetOrders_WhenPlaceOrder()
        {
            var productOrderDummy = new Mock<IProductionOrder>();
            var orderStoreStub = new Mock<IOrdersStore>();
            var productOrder = productOrderDummy.Object;
            var orderStore = orderStoreStub.Object;
            var trackerClient = new TrackerClient(orderStore);

            trackerClient.PlaceOrder(productOrder);

            orderStoreStub.Verify(m => m.GetOrders(), Times.Never);
        }

    
    }
}
