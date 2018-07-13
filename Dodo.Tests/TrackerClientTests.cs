using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

            var johnResult = trackerClient.GetOrderByName("John");
            var tomResult = trackerClient.GetOrderByName("Tom");

            Assert.Equal(3, johnResult.Number);
            Assert.Equal(2, tomResult.Number);
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

            Assert.True(orderDateAdd != orderUpdateDate);
        }

        [Fact]
        public void OnAddProductionOrderWithExitingOrder_CallUpdateProductionOrder()
        {
            var orderStorageMock = new Mock<IOrdersStorage>();
            orderStorageMock.Setup(o => o.GetProductionOrderByName(It.IsAny<string>())).Returns(new ProductionOrder());            

            var trackerClient = new TrackerClient(orderStorageMock.Object);

            trackerClient.AddProductionOrder("John", 1);

            orderStorageMock.Verify(o => o.UpdateProductionOrder(
                It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int?>()), Times.Once);

        }
        [Fact]
        public void OnAddProductionOrderWithoutExitingOrder_CallAddProductionOrder()
        {
            var orderStorageMock = new Mock<IOrdersStorage>();
            orderStorageMock.Setup(o => o.GetProductionOrderByName(It.IsAny<string>())).Returns<ProductionOrder>(null);

            var trackerClient = new TrackerClient(orderStorageMock.Object);

            //Убираем предыдущие вызовы, так как есть дефолтные значения в конструкторе
            orderStorageMock.ResetCalls();

            trackerClient.AddProductionOrder("John", 1);

            orderStorageMock.Verify(o => o.AddProductionOrder(
               It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void ReturnsOrderWithDateAfter_WhenGetOrdersAfterDate_()
        {
            var orderStorageMock = new Mock<IOrdersStorage>();

            var expectedOrder = new ProductionOrder { ChangeDate = new DateTime(2018, 12, 01) };

            orderStorageMock.Setup(o => o.GetAllProductionOrders())
                .Returns(new List<ProductionOrder>
                {
                    new ProductionOrder { ChangeDate = new DateTime(2018, 01, 01)},
                    new ProductionOrder { ChangeDate = new DateTime(2018, 01, 01)},
                    new ProductionOrder { ChangeDate = new DateTime(2018, 01, 01)},
                    expectedOrder,
                });

            var trackerClient = new TrackerClient(orderStorageMock.Object);

            var orders = trackerClient.GetOrdersAfterDate(new DateTime(2018, 06, 01));

            Assert.Single(orders);
            Assert.Equal(expectedOrder, orders[0]);
        }

        private IOrdersStorage GetOrderStorageTest(IDateTimeProvider dateTimeProvider = null)
        {
            return new InMemoryOrdersStorage(dateTimeProvider ?? new DateTimeProvider());
        }
    }
}
