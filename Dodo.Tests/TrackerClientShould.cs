using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;


namespace Dodo.Tests
{
    public class TrackerClientShould
    {
        [Fact]
        public void ReturnAllOrders_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var orders = 2.Orders();
            var trackerClient = Create.TrackerClient().With(orders);

            var receivedOrders = Get.OrdersFrom(trackerClient).Please();

            AssertThat.The(receivedOrders).EqualTo(orders);
        }

        [Fact]
        public void ReturnOnlyExpiringOrders_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var date = Create.Date(11, 07, 2018).WithTime(23, 00).Please();
            var notExpiringOrder = Create.Order().CreatedOnDate(WhichIs.EarlierThan(date, For.PeriodOf(1.Hour()))).Please();
            var expiringOrder = Create.Order().CreatedOnDate(WhichIs.EarlierThan(date, For.PeriodOf(1.Hour().And(1.Minute())))).Please();
            var trackerClient = Create.TrackerClient().With(expiringOrder).And().With(notExpiringOrder);
            
            var receivedOrders = Get.OrdersFrom(trackerClient).ExpiringOn(date).Please();

            AssertThat.The(receivedOrders).EqualTo(expiringOrder);
        }

        [Fact]
        public void CallGetOrdersOnOrdersProvider_WhenGetOrdersIsCalled()
        {
            var ordersProvider = Create.MockForOrdersProvider();
            var trackerClient = Create.TrackerClient().With(ordersProvider);

            Get.OrdersFrom(trackerClient).Please();

            VerifyThat.GetOrdersMethodIsCalledOn(ordersProvider, 1.Times());
        }                

        [Fact]
        public void NotCallNowOnDateProvider_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var dateProvider = Create.MockForDateProvider();
            var trackerClient = Create.TrackerClient().With(dateProvider);
            
            Get.OrdersFrom(trackerClient).Please();

            VerifyThat.NowMethodIsCalledOn(dateProvider, 0.Times());
        }
        

        [Fact]
        public void CallNowOnDateProvider_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var dateProvider = Create.MockForDateProvider();
            var trackerClient = Create.TrackerClient().With(dateProvider);
            
            Get.ExpiringOnlyOrdersFrom(trackerClient).Please();

            VerifyThat.NowMethodIsCalledOn(dateProvider, 1.Times());
        }
    }    
}

    