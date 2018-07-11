using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dodo.RestaurantBoard.Domain.Services
{
    class InMemoryOrderStorage : IOrdersStorage
    {
       private  int lastOrderId = 0;
       private Dictionary<int,ProductionOrder> productionOrders = new Dictionary<int, ProductionOrder>();


        public void AddProductionOrder(string clientName, int number)
        {
            lastOrderId++;
            productionOrders.Add(lastOrderId, new ProductionOrder()
            {
                Id = lastOrderId,
                ClientName = clientName,
                Number = number
            }
                );
        }

        public void DeleteProductionOrder(int id)
        {
            productionOrders.Remove(id);
        }

        public ProductionOrder[] GetAllProductionOrders()
        {
            return productionOrders.Values.ToArray();
        }

        public ProductionOrder GetProductionOrderById(int id)
        {

            if(productionOrders.TryGetValue(id, out var productionOrder))
            {
                return productionOrder;
            }

            return null;
        }

        public void UpdateProductionOrder(int id, string clientName = null, int? number = null)
        {
            if (productionOrders.TryGetValue(id, out var productionOrder))
            {
                productionOrder.ClientName = clientName ?? productionOrder.ClientName;
                productionOrder.Number = number ?? productionOrder.Number;
            }
        }
    }
}
