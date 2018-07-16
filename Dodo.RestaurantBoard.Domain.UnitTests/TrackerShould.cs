using System;
using System.Linq;
using Moq;
using Xunit;
using Dodo.RestaurantBoard.Domain.Services;

namespace Dodo.RestaurantBoard.Domain.UnitTests
{
    public class TrackerShould
    {
        #region State tests
        
        [Fact]
        public void IncrementId_WhenOrderIsPlaced()
        {
            var tracker = new TrackerClient();
            tracker.AddOrder(clientName: "Михаил");
           
            Assert.True(tracker.Orders.Last().Id == 1);

            tracker.AddOrder(clientName: "Алексей");
            Assert.True(tracker.Orders.Last().Id == 2);
        }
        
        [Fact]
        public void RetainOrder_WhenOrderIsPlaced()
        {
            var tracker = new TrackerClient();
            
            tracker.AddOrder(clientName: "Михаил");

            Assert.True(tracker.NumberOfOrders == 1);
        }

        [Fact]
        public void ReturnProductionOrder_WhenOrderWithInputIdentifierIsExist()
        {
            var tracker = new TrackerClient();
            var newOrderId = tracker.AddOrder(clientName: "Михаил");

            var order = tracker.GetOrder(newOrderId);
            
            Assert.NotNull(order);
        }
        
        [Fact]
        public void ThrowException_WhenNoOrderIsFound()
        {
            var tracker = new TrackerClient();

            Action act = () => tracker.GetOrder(1);
            
            Assert.ThrowsAny<Exception>(act);
        }
        
        #endregion
        
        #region Behavior tests
        [Fact]
        public void UpdateVisibleOrders_WhenNewOrderIsPlaced()
        {
            var trackerPresenterMock = new Mock<TrackerPresenter>();
            var tracker = new TrackerClient(presenter: trackerPresenterMock.Object);
            
            tracker.AddOrder(clientName: "Катя");
            
            trackerPresenterMock.Verify(x => x.UpdateListOfVisibleOrders(), Times.Once);
        }

        [Fact]
        public void UpdateVisibleOrders_WhenOrderIsRemoved()
        {
            var trackerPresenterMock = new Mock<TrackerPresenter>();
            var tracker = new TrackerClient(presenter: trackerPresenterMock.Object);
            var orderId = tracker.AddOrder(clientName: "Катя");
            
            tracker.RemoveOrder(orderId);
            
            trackerPresenterMock.Verify(x => x.UpdateListOfVisibleOrders(), Times.Exactly(2));
        }

        [Fact]
        public void Beep_WhenOrderIsPlaced()
        {
            var beepServiceMock = new Mock<BeepService>();
            var tracker = new TrackerClient(beepService: beepServiceMock.Object);
            
            tracker.AddOrder(clientName: "Катя");
            
            beepServiceMock.Verify(x => x.Beep(), Times.Once);
        }
        
        [Fact]
        public void NotBeep_WhenOrderIsRemoved()
        {
            var beepServiceMock = new Mock<BeepService>();
            var tracker = new TrackerClient(beepService: beepServiceMock.Object);
            var orderId = tracker.AddOrder(clientName: "Катя");
            
            tracker.RemoveOrder(orderId);
            
            beepServiceMock.Verify(x => x.Beep(), Times.Exactly(1));
        }
        #endregion
    }
}