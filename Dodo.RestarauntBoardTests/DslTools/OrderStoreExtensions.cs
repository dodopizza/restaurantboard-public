using System;
using Dodo.RestaurantBoard.Domain.Stores;

namespace Dodo.RestarauntBoardTests.DslTools
{
    public static class OrderStoreExtensions
    {
        public static void GetExpiredOrders(this OrdersStore orderStore, string dateString)
        {
            var date = DateTime.Parse(dateString);
            orderStore.GetExpiredOrders(date);
        }
    }
}