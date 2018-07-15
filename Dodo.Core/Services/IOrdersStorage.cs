﻿using Dodo.Tracker.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.Core.Services
{
    public interface IOrdersStorage
    {
        IEnumerable<ProductionOrder> GetAllProductionOrders();

        ProductionOrder GetProductionOrderById(int id);

        void AddProductionOrder(string clientName, int number);

        void DeleteProductionOrder(int id);

        void UpdateProductionOrderName(int id, string clientName);

        void UpdateProductionOrderNumber(int id, int number);

        ProductionOrder GetProductionOrderByName(string clientName);
    }
}
