using Dodo.Tracker.Contracts;
using System;

namespace Dodo.Tests.DSL
{
    public class ProductionOrderBuilder
    {
        private readonly ProductionOrder _order;

        public ProductionOrderBuilder()
        {
            _order = new ProductionOrder();
        }

        internal ProductionOrderBuilder WithChangeDate(DateTime changeDate)
        {
            _order.ChangeDate = changeDate;

            return this;
        }

        internal ProductionOrder Please()
        {
            return _order;
        }
    } 
}
