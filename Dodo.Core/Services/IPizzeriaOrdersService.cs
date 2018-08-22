using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.DomainModel.OrderProcessing;
using System.Threading.Tasks;

namespace Dodo.Core.Services
{
    public interface IPizzeriaOrdersService
    {
        Task<RestaurantReadnessOrders[]> GetOrders(Pizzeria pizzeria);
    }
}
