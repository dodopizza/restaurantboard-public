using Dodo.Tracker.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.Core.Services
{
    public interface IOrdersStorage
    {
        ProductionOrder[] GetAllProductionOrders();

        ProductionOrder GetProductionOrderById(int id);

        void AddProductionOrder(string clientName, int number);

        void DeleteProductionOrder(int id);

        void UpdateProductionOrder(int id, string clientName = null, int? number = null);

        ProductionOrder GetProductionOrderByName(string clientName);
    }
}
