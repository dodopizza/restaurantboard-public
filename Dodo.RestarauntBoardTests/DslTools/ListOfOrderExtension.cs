﻿using System.Collections.Generic;
using System.Linq;
using Dodo.Tracker.Contracts;

namespace Dodo.RestarauntBoardTests.DslTools
{
    public static class ListOfOrderExtension
    {
        public static bool Contains(this List<IProductionOrder> list, params ProductionOrder[] orders)
        {
            return orders.All(list.Contains);
        }

        public static bool EqualsWith(this List<IProductionOrder> list, ProductionOrder order)
        {
            return list.Contains(order);
        }
    }
}