using System;
using System.Collections.Generic;
using System.Text;
using Dodo.Core.Common;
using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    public static class Create
    {
        public static BoardContollerBuilder BoardController => new BoardContollerBuilder();

        public static Mock<ITrackerClient> TrackerClientStub(ProductionOrder[] productionOrders)
        {
            var trackerClient = new Mock<ITrackerClient>();
            trackerClient
                .Setup(m => m.GetOrdersByTypeAsync(It.IsAny<Uuid>(), It.IsAny<OrderType>(), It.IsAny<int>()))
                .ReturnsAsync(productionOrders);
            return trackerClient;
        }
    }
}
