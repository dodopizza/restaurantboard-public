using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Dodo.RestaurantBoard.Domain.Services
{
    public class InMemoryOrdersStorage : IOrdersStorage
    {
        private  int lastOrderId = 0;
        private Dictionary<int, ProductionOrder> productionOrders = new Dictionary<int, ProductionOrder>();
        private readonly IDateTimeProvider dateTimeProvider;

        public InMemoryOrdersStorage(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public void AddProductionOrder(string clientName, int number)
        {
            lastOrderId++;
            productionOrders.Add(lastOrderId, new ProductionOrder()
            {
                Id = lastOrderId,
                ClientName = clientName,
                Number = number,
                ChangeDate = dateTimeProvider.GetDateTime()
            });
        }

        public void DeleteProductionOrder(int id)
        {
            productionOrders.Remove(id);
        }

        public IEnumerable<ProductionOrder> GetAllProductionOrders()
        {
            return productionOrders.Values;
        }

        public ProductionOrder GetProductionOrderById(int id)
        {

            if(productionOrders.TryGetValue(id, out var productionOrder))
            {
                return productionOrder;
            }

            return null;
        }

        public void UpdateProductionOrderName(int id, string clientName)
        {
            if (productionOrders.TryGetValue(id, out var productionOrder))
            {
                productionOrder.ClientName = clientName;
                productionOrder.ChangeDate = dateTimeProvider.GetDateTime();
            }
        }

        public void UpdateProductionOrderNumber(int id, int number)
        {
            if (productionOrders.TryGetValue(id, out var productionOrder))
            {
                productionOrder.Number = number;
                productionOrder.ChangeDate = dateTimeProvider.GetDateTime();
            }
        }

        public ProductionOrder GetProductionOrderByName(string clientName)
        {
            return  productionOrders.FirstOrDefault(pair => pair.Value.ClientName == clientName).Value;
        }
    }
}
