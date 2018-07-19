using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.Tests.DSL
{
    public class OrderStorageStubBuilder
    {
        private Mock<IOrdersStorage> _orderStorageStub;

        public OrderStorageStubBuilder()
        {
            _orderStorageStub = new Mock<IOrdersStorage>();
        } 

        public OrderStorageStubBuilder WithExistingOrders(params ProductionOrder[] orders)
        {
            _orderStorageStub.Setup(o => o.GetAllProductionOrders()).Returns(orders);
            return this;
        }

        public IOrdersStorage RightNow()
        {
            return _orderStorageStub.Object;
        }
    }
}
