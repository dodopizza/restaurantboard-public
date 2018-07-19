using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using System;
using System.Collections.Generic;
using FluentAssertions.Extensions;
using Xunit;


namespace Dodo.Tests
{
    public class TrackerClientShould
    {
        [Fact]
        public void ReturnAllOrders_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var orders = 2.Orders();
            var trackerClient = Create.TrackerClient().With(orders).Please();

            var receivedOrders = trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0);

            AssertThat.The(receivedOrders).EqualTo(orders);
        }

        [Fact]
        public void ReturnOnlyExpiringOrders_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var date = 11.July(2018).WithTime(23, 00).Please();
            var notExpiringOrder = Create.Order().CreatedOnDate(WhichIs.EarlierThan(date, For.PeriodOf(1.Hour()))).Please();
            var expiringOrder = Create.Order().CreatedOnDate(WhichIs.EarlierThan(date, For.PeriodOf(1.Hour().And(1.Minute())))).Please();
            var dateProvider = Create.MockForDateProviderAlwaysReturning(date);
            var trackerClient = Create.TrackerClient().With(expiringOrder).And().With(notExpiringOrder).And().With(dateProvider).Please();
            
            var receivedOrders = trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0, true);

            AssertThat.The(receivedOrders).EqualTo(expiringOrder);
        }

        [Fact]
        public void CallGetOrdersOnOrdersProvider_WhenGetOrdersIsCalled()
        {
            var ordersProvider = Create.MockForOrdersProvider();
            var trackerClient = Create.TrackerClient().With(ordersProvider).Please();

            trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0);

            VerifyThat.GetOrdersMethodIsCalledOn(ordersProvider, 1.Times());
        }                

        [Fact]
        public void NotCallNowOnDateProvider_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var dateProvider = Create.MockForDateProvider();
            var trackerClient = Create.TrackerClient().With(dateProvider).Please();
            
            trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0);

            VerifyThat.NowMethodIsCalledOn(dateProvider, 0.Times());
        }
        

        [Fact]
        public void CallNowOnDateProvider_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var dateProvider = Create.MockForDateProvider();
            var trackerClient = Create.TrackerClient().With(dateProvider).Please();
            
            trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0, true);

            VerifyThat.NowMethodIsCalledOn(dateProvider, 1.Times());
        }
    }    
}

    