using System;
using Dodo.Tracker.Contracts;

namespace Dodo.RestarauntBoardTests.DslTools
{
    public static class ProductioOrderExtensions
    {
        public static DateTime ExpireDate(this IProductionOrder order)
        {
            return order.OrderDate.AddSeconds((new ProductionOrder()).ExpirationTime + 1);
        }

        public static DateTime Date(this IProductionOrder order)
        {
            return order.OrderDate;
        }
    }
}