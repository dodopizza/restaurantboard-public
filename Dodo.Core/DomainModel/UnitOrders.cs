using System.Collections.Generic;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;

namespace Dodo.Core.DomainModel
{
    public class UnitOrders
    {
        public Pizzeria Unit { get; set; }
        public List<RestaurantReadnessOrders> Orders { get; set; }
    }
}