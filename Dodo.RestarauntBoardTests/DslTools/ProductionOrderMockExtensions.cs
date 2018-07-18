using System;
using Dodo.Tracker.Contracts;
using Moq;

namespace Dodo.RestarauntBoardTests.DslTools
{
    public static class ProductionOrderMockExtensions
    {
        public static void IsExpiredWasCalledOnce(this Mock<IProductionOrder> orderMock)
        {
            orderMock.Verify(p => p.IsExpired(It.IsAny<DateTime>()), Times.Once);
        }

        public static void IsExpiredWasNeverCalled(this Mock<IProductionOrder> orderMock)
        {
            orderMock.Verify(p => p.IsExpired(It.IsAny<DateTime>()), Times.Never);
        }
    }
}