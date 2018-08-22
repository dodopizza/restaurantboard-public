using Dodo.Core.DomainModel.OrderProcessing;

namespace Dodo.Core.DomainModel.Pizzeria
{
    public class PizzeriaOrders
    {
        public Departments.Units.Pizzeria Pizzeria { get; set; }

        public RestaurantReadnessOrders[] Orders { get; set; }
    }
}
