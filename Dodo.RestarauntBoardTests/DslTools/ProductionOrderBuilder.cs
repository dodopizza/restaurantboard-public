using System;
using Dodo.Tracker.Contracts;

namespace Dodo.RestarauntBoardTests.DslTools
{
    public class ProductionOrderBuilder
    {
        private ProductionOrder _order;

        public ProductionOrderBuilder()
        {
            _order = new ProductionOrder();
        }

        internal ProductionOrderBuilder WithDate(DateTime date)
        {
            _order.OrderDate = date;
            return this;
        }

        internal ProductionOrder Please()
        {
            return _order;
        }
    }
}