using System.Threading.Tasks;
using Dodo.Core.DomainModel.Pizzeria;

namespace Dodo.Core.Services
{
    public interface IPizzeriaOrdersService
    {
        Task<PizzeriaOrders> GetOrdersByUnitIdAsync(int unitId);
    }
}