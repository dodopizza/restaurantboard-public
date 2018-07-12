using Dodo.Tracker.Contracts;
namespace Dodo.RestaurantBoard.Domain.Services
{
    public interface IOrdersRepository
    {
        ProductionOrder[] GetOrders();
    }
}
