using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dodo.Tests
{
    public class TrackerClientTests
    {
        [Fact]
        public void UpdateOrderNumber_WhenAddingOrderWithSameName()
        {
            var trackerClient = new TrackerClient(GetOrderStorageTest());

            trackerClient.AddProductionOrder("John", 3);
            trackerClient.AddProductionOrder("John", 2);

            Assert.Equal(3 + 2, trackerClient.GetOrderByName("John").Number);
        }

        [Fact]
        public void AddingNewOrder_WhenAddingWithDifferentClientNames()
        {
            var trackerClient = new TrackerClient(GetOrderStorageTest());

            trackerClient.AddProductionOrder("John", 3);
            trackerClient.AddProductionOrder("Tom", 2);

            var tomResult = trackerClient.GetOrderByName("Tom");

            Assert.Equal(2, tomResult.Number);
        }

        [Fact]
        public void NotChangingOldOrder_WhenAddingWithDifferentClientNames()
        {
            var trackerClient = new TrackerClient(GetOrderStorageTest());

            trackerClient.AddProductionOrder("John", 3);
            trackerClient.AddProductionOrder("Tom", 2);

            var johnResult = trackerClient.GetOrderByName("John");

            Assert.Equal(3, johnResult.Number);
        }

        [Fact]
        public void SetChangeDateFromDateTimeProvider_WhenAddingOrder()
        {
            var dateTimeExpected = new DateTime(2018, 1, 1, 1, 1, 1);
            var dateTimeProviderStub = new Mock<IDateTimeProvider>();
            dateTimeProviderStub.Setup(d => d.GetDateTime()).Returns(dateTimeExpected);
            var trackerClient = new TrackerClient(GetOrderStorageTest(dateTimeProviderStub.Object));

            trackerClient.AddProductionOrder("John", 3);
            
            var orderAddDate = trackerClient.GetOrderByName("John").ChangeDate;

            Assert.Equal(dateTimeExpected, orderAddDate);
        }

        [Fact]
        public void UpdateChangeDate_WhenUpdatingOrder()
        {
            var dateTimeCreated = new DateTime(2018, 1, 1, 1, 1, 1);
            var dateTimeProviderStub = new Mock<IDateTimeProvider>();

            var trackerClient = new TrackerClient(GetOrderStorageTest(dateTimeProviderStub.Object));
            dateTimeProviderStub.SetupSequence(d => d.GetDateTime())
                .Returns(dateTimeCreated)
                .Returns(dateTimeCreated.AddSeconds(10));

            trackerClient.AddProductionOrder("John", 3);
            var orderDateAdd = trackerClient.GetOrderByName("John").ChangeDate;

            trackerClient.AddProductionOrder("John", 2);

            var orderUpdateDate = trackerClient.GetOrderByName("John").ChangeDate;

            Assert.True(orderDateAdd.Value.AddSeconds(10) == orderUpdateDate.Value);
        }

        [Fact]
        public void ReturnsExpectedOrders_WhenGetOrdersAfterDate()
        {
            var orderStorageStub = new Mock<IOrdersStorage>();

            var expectedOrders = new[] { new ProductionOrder { ChangeDate = new DateTime(2018, 12, 01) } };
            var allOrders = new List<ProductionOrder>(expectedOrders);
            allOrders.Add(new ProductionOrder { ChangeDate = new DateTime(2018, 01, 01) });

            orderStorageStub.Setup(o => o.GetAllProductionOrders()).Returns(allOrders);

            var trackerClient = new TrackerClient(orderStorageStub.Object);

            var orders = trackerClient.GetOrdersAfterDate(new DateTime(2018, 06, 01));

            Assert.Equal(expectedOrders, orders);
        }

        [Fact]
        public void OnAddProductionOrderWithExistingOrder_CallUpdateProductionOrder()
        {
            var orderStorageMock = new Mock<IOrdersStorage>();
            orderStorageMock.Setup(o => o.GetProductionOrderByName(It.IsAny<string>()))
                .Returns(new ProductionOrder() { Id = 5, Number = 3 });            

            var trackerClient = new TrackerClient(orderStorageMock.Object);

            trackerClient.AddProductionOrder("John", 1);

            orderStorageMock.Verify(o => o.UpdateProductionOrder(5, null, 1 + 3), Times.Once);

        }
        [Fact]
        public void OnAddProductionOrderWithoutExistingOrder_CallAddProductionOrder()
        {
            var orderStorageMock = new Mock<IOrdersStorage>();
            orderStorageMock.Setup(o => o.GetProductionOrderByName(It.IsAny<string>())).Returns<ProductionOrder>(null);

            var trackerClient = new TrackerClient(orderStorageMock.Object);            

            trackerClient.AddProductionOrder("John", 1);

            orderStorageMock.Verify(o => o.AddProductionOrder("John", 1), Times.Once);
        }

        private IOrdersStorage GetOrderStorageTest(IDateTimeProvider dateTimeProvider = null)
        {
            return new InMemoryOrdersStorage(dateTimeProvider ?? new DateTimeProviderUtcNow());
        }
    }
}
