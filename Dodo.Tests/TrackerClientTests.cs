using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tests.DSL;
using Dodo.Tracker.Contracts;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Dodo.Tests
{
    public class TrackerClientTests
    {
        [Fact]
        public void UpdateOrderNumber_WhenAddingOrderWithSameName()
        {
            var trackerClient = Gimmy.TrackerClient().Default().WithExistingOrder("John", 3).RightNow();

            trackerClient.AddProductionOrder("John", 2);

            trackerClient.NumberFor("John").ShouldBe(3 + 2);
        }

        [Fact]
        public void AddingNewOrder_WhenAddingWithDifferentClientNames()
        {
            var trackerClient = Gimmy.TrackerClient().Default().WithExistingOrder("John", 3).RightNow();

            trackerClient.AddProductionOrder("Tom", 2);

            trackerClient.NumberFor("Tom").ShouldBe(2);
        }

        [Fact]
        public void NotChangingOldOrder_WhenAddingWithDifferentClientNames()
        {
            var trackerClient = Gimmy.TrackerClient().Default().WithExistingOrder("John", 3).RightNow();

            trackerClient.AddProductionOrder("Tom", 2);

            trackerClient.NumberFor("John").ShouldBe(3);
        }

        [Fact]
        public void SetChangeDateFromDateTimeProvider_WhenAddingOrder()
        {
            var dateTimeProviderStub = Gimmy.DateTimeProviderStub(1.January(2018)).RightNow();
            var orderStorage = Gimmy.OrderStorage().WithDateTimeProvider(dateTimeProviderStub).RightNow();
            var trackerClient = Gimmy.TrackerClient().WithOrderStorage(orderStorage).RightNow();

            trackerClient.AddProductionOrder("John", 3);

            trackerClient.ChangeDateFor("John").ShouldBe(1.January(2018));
        }

        [Fact]
        public void UpdateChangeDate_WhenUpdatingOrder()
        {
            var dateTimeProviderStub = Gimmy.DateTimeProviderStub().WithDates(1.January(2018), 2.January(2018)).RightNow();
            var orderStorage = Gimmy.OrderStorage().WithDateTimeProvider(dateTimeProviderStub).RightNow();
            var trackerClient = Gimmy.TrackerClient().WithOrderStorage(orderStorage).RightNow();

            trackerClient.AddProductionOrder("John", 3);
            var orderDateAdd = trackerClient.ChangeDateFor("John");

            trackerClient.AddProductionOrder("John", 2);
            var orderUpdateDate = trackerClient.ChangeDateFor("John");

            Difference.Between(orderDateAdd).And(orderUpdateDate).ShouldBe(1.Days());
        }

        [Fact]
        public void ReturnsExpectedOrders_WhenGetOrdersAfterDate()
        {
            var orderStorageStub = new Mock<IOrdersStorage>();

            var expectedOrders = new[] { new ProductionOrder { ChangeDate = 1.December(2018) } };
            var allOrders = new List<ProductionOrder>(expectedOrders);
            allOrders.Add(new ProductionOrder { ChangeDate = 1.January(2018) });

            orderStorageStub.Setup(o => o.GetAllProductionOrders()).Returns(allOrders);

            var trackerClient = new TrackerClient(orderStorageStub.Object);

            var orders = trackerClient.GetOrdersAfterDate(1.June(2018));

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

            orderStorageMock.Verify(o => o.UpdateProductionOrderNumber(5, 1 + 3), Times.Once);

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
