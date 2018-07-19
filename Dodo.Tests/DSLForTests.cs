

using System;
using System.Collections.Generic;
using Dodo.Core.Common;
using Dodo.RestaurantBoard.Domain.Services;
using Dodo.Tracker.Contracts;
using Dodo.Tracker.Contracts.Enums;
using Moq;
using Xunit;

namespace Dodo.Tests
{
    public static class Extensions
    {
        public static ProductionOrder[] Orders(this int orderCount)
        {
            var orders = new List<ProductionOrder>();
            for (int i = 0; i < orderCount; i++)
            {
                orders.Add(new ProductionOrder());
            }

            return orders.ToArray();
        }
        
        public static TimeSpan Hour(this int hour)
        {
            return new TimeSpan(hour, 0, 0);
        }       
        
        public static TimeSpan Minute(this int minute)
        {
            return new TimeSpan(0, minute, 0);
        }

        public static TimeSpan And(this TimeSpan timeSpan, TimeSpan timeSpanAdd)
        {
            return timeSpan + timeSpanAdd;
        }
        
        public static int Times(this int times)
        {
            return times;
        }
        
        public static DateTimeBuilder July(this int day, int year)
        {
            return new DateTimeBuilder(day, 7, year);
        }
    }

    public static class Create
    {
        public static TrackerClientBuilder TrackerClient()
        {
            return new TrackerClientBuilder();
        }

        public static OrderBuilder Order()
        {
            return new OrderBuilder();
        }

        public static DateTimeBuilder Date(int day, int month, int year)
        {
           return  new DateTimeBuilder(day, month, year);
        }

        public static Mock<IOrdersProvider> MockForOrdersProvider()
        {
            return new Mock<IOrdersProvider>();
        }

        public static Mock<IDateProvider> MockForDateProvider()
        {
            return new Mock<IDateProvider>();
        }
        
        public static Mock<IDateProvider> MockForDateProviderAlwaysReturning(DateTime date)
        {
            var dateProviderMock = MockForDateProvider();
            dateProviderMock.Setup(dp => dp.Now()).Returns(date);
            return dateProviderMock;
        }
    }

    public class OrderBuilder
    {
        private DateTime _creationDate = DateTime.Now;
        internal OrderBuilder CreatedOnDate(DateTime date)
        {
            _creationDate = date;
            return this;
        }

        public ProductionOrder Please()
        {
            return new ProductionOrder()
            {
                ChangeDate = _creationDate
            };
        }
    }

    public static class Get
    {
        public static GetOrdersCallBuilder OrdersFrom(TrackerClientBuilder trackerClientBuilder)
        {
            return new GetOrdersCallBuilder(trackerClientBuilder);
        }       

        public static GetOrdersCallBuilder ExpiringOnlyOrdersFrom(TrackerClientBuilder trackerClientBuilder)
        {
            return new GetOrdersCallBuilder(trackerClientBuilder).ExpiringOnly();
        }
    }

    public class GetOrdersCallBuilder
    {
        private readonly TrackerClientBuilder _trackerClientBuilder;  
        private bool _expiringOnly;

        public GetOrdersCallBuilder(TrackerClientBuilder builder)
        {
            _trackerClientBuilder = builder;
        }

        internal GetOrdersCallBuilder ExpiringOn(DateTime date)
        {
            _expiringOnly = true;
            var dateProvider = new Mock<IDateProvider>();
            dateProvider.Setup(p => p.Now()).Returns(date);
            _trackerClientBuilder.With(dateProvider.Object);
            return this;
        }
        
        internal GetOrdersCallBuilder ExpiringOnly()
        {
            _expiringOnly = true;
            return this;
        }
        
        public ProductionOrder[] Please()
        {
            var trackerClient = _trackerClientBuilder.Please();
            return trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0, _expiringOnly);
        }
    }

    public class TrackerClientBuilder
    {
        private readonly List<ProductionOrder> _orders = new List<ProductionOrder>();
        private IDateProvider _dateProvider = new DateProvider();
        private Mock<IOrdersProvider> _ordersProvider = new Mock<IOrdersProvider>();

        public TrackerClientBuilder With(ProductionOrder[] productionOrders)
        {
            _orders.AddRange(productionOrders);
            return this;
        }

        public TrackerClientBuilder With(ProductionOrder order)
        {
            _orders.Add(order);
            return this;
        }
        
        public TrackerClientBuilder With(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
            return this;
        }

        public TrackerClientBuilder And()
        {
            return this;
        }
        
        public TrackerClient Please()
        {
            
            _ordersProvider.Setup(p => p.GetOrders()).Returns(_orders.ToArray());
            return new TrackerClient(_ordersProvider.Object, _dateProvider);
        }

        public TrackerClientBuilder With(Mock<IOrdersProvider> ordersProvider)
        {
            _ordersProvider = ordersProvider;
            return this;
        }
        public TrackerClientBuilder With(Mock<IDateProvider> dateProvider)
        {
            _dateProvider = dateProvider.Object;
            return this;
        }
    }

    public static class AssertThat
    {
        public static AssertBuilder The(ProductionOrder[] orders)
        {
            return new AssertBuilder(orders);
        }

        public static AssertBuilder The(ProductionOrder order)
        {
            return new AssertBuilder( new[] { order });
        }
    }

    public class AssertBuilder
    {
        private readonly ProductionOrder[] _orders;

        public AssertBuilder(ProductionOrder[] orders)
        {
            _orders = orders;
        }

        public void EqualTo(ProductionOrder[] otherOrders)
        {
            Assert.Equal(otherOrders, _orders);
        }

        public void EqualTo(ProductionOrder otherOrder)
        {
            Assert.Equal(new[] { otherOrder }, _orders);
        }
    }
    
    public static class VerifyThat
    {
        public static void GetOrdersMethodIsCalledOn(Mock<IOrdersProvider> ordersProvider, int times)
        {
            ordersProvider.Verify(op => op.GetOrders(), Times.Exactly(times));
        }

        public static void NowMethodIsCalledOn(Mock<IDateProvider> dateProvider, int times)
        {
            dateProvider.Verify(dp => dp.Now(), Times.Exactly(times));
        }
    }

    public static class WhichIs
    {        
        internal static DateTime EarlierThan(DateTime date, TimeSpan timeSpan)
        {
            return date - timeSpan;
        }
    }

    public static class For
    {        
        public static TimeSpanBuilder PeriodOf(TimeSpan timeSpan)
        {
            return new TimeSpanBuilder(timeSpan);
        }        
    }

    public class TimeSpanBuilder
    {
        private readonly TimeSpan _timeSpan;

        public TimeSpanBuilder(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }
        
        public static implicit operator TimeSpan(TimeSpanBuilder builder)
        {
            return builder._timeSpan;
        }
    }

    public class DateTimeBuilder
    {
        private readonly int _day, _month, _year;
        private int _hour, _minute, _seconds;


        public DateTimeBuilder(int day, int month, int year)
        {
            _day = day;
            _month = month;
            _year = year;
        }
        
        
        public DateTimeBuilder WithTime(int hour, int minute, int seconds =0)
        {
            _hour = hour;
            _minute = minute;
            _seconds = seconds;
            return this;
        }

        public DateTime Please()
        {
            return new DateTime(_year, _month, _day, _hour, _minute , _seconds);
        }
        
        public static implicit operator DateTime(DateTimeBuilder builder)
        {
            return builder.Please();
        }
    }
}