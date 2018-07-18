using Dodo.Tracker.Contracts;
using Moq;

namespace Dodo.RestarauntBoardTests.DslTools
{
    public class ProductionOrderMockBuilder
    {
        private Mock<IProductionOrder> _orderMock;

        public ProductionOrderMockBuilder()
        {
            _orderMock = new Mock<IProductionOrder>();
        }

        internal IProductionOrder GetMockObject()
        {
            return _orderMock.Object;
        }

        public Mock<IProductionOrder> VerifyThat => _orderMock;
    }
}