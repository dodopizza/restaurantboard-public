using System.Collections.Generic;
using Dodo.Tracker.Contracts;
namespace Dodo.RestaurantBoard.Domain.Services
{
    public interface IOrdersRepository
    {
        IEnumerable<ProductionOrder> GetOrders();
        void AddOrder(ProductionOrder order);
        void DeleteOrder(int id);
    }
}