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
            var trackerClient = new TrackerClient(new InMemoryOrdersStorage());

            trackerClient.AddProductionOrder("John", 3);
            trackerClient.AddProductionOrder("John", 2);

            var result = trackerClient.GetOrderByName("John");

            Assert.Equal(5, result.Number);
        }

        [Fact]
        public void AddingNewOrder_WhenAddingWithDifferentClientNames()
        {
            var trackerClient = new TrackerClient(new InMemoryOrdersStorage());

            trackerClient.AddProductionOrder("John", 3);
            trackerClient.AddProductionOrder("Tom", 2);

            var johnResult = trackerClient.GetOrderByName("John");
            var tomResult = trackerClient.GetOrderByName("Tom");

            Assert.Equal(3, johnResult.Number);
            Assert.Equal(2, tomResult.Number);
        }

        [Fact]
        public void SetChangeDateToUtcNow_WhenAddingOrder()
        {
            var trackerClient = new TrackerClient(new InMemoryOrdersStorage());

            var dateBeforeCall = DateTime.UtcNow;

            trackerClient.AddProductionOrder("John", 3);

            var dateAfterCall = DateTime.UtcNow;

            var orderAddDate = trackerClient.GetOrderByName("John").ChangeDate;

            Assert.True(orderAddDate >= dateBeforeCall && orderAddDate <= dateAfterCall);
        }

        [Fact]
        public void UpdateChangeDateToNewer_WhenUpdatingOrder()
        {
            var trackerClient = new TrackerClient(new InMemoryOrdersStorage());
            trackerClient.AddProductionOrder("John", 3);
            var orderDateAdd = trackerClient.GetOrderByName("John").ChangeDate;

            Task.Delay(10).Wait();

            trackerClient.AddProductionOrder("John", 2);

            var orderUpdateDate = trackerClient.GetOrderByName("John").ChangeDate;

            Assert.True(orderDateAdd < orderUpdateDate);
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
    }
}
