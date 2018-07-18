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

        //public static GetOrdersCallBuilder ExpiringOrdersFrom(ITrackerClient trackerClient)
        //{
        //    return new GetOrdersCallBuilder(trackerClient, true);
        //}
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
        
        public ProductionOrder[] Please()
        {
            var trackerClient = _trackerClientBuilder.Please();
            return trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0, _expiringOnly);
        }
    }

    public class TrackerClientBuilder
    {
        private List<ProductionOrder> _orders = new List<ProductionOrder>();
        private IDateProvider _dateProvider = new DateProvider();

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
            var ordersProviderStub = new Mock<IOrdersProvider>();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(_orders.ToArray());
            return new TrackerClient(ordersProviderStub.Object, _dateProvider);
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
        private ProductionOrder[] _orders;

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

    public static class WhichIs
    {        
        internal static DateTimeBuilder EarlierThan(DateTime date)
        {

            return new DateTimeBuilder(date, true);
        }
    }

    class DateTimeBuilder
    {
        private DateTime _date;
        private readonly bool _isEarlier;

        public DateTimeBuilder(DateTime initialDate, bool isEarlier)
        {
            _date = initialDate;
            _isEarlier = isEarlier;
        }

        public DateTimeBuilder For(TimeSpan timeSpan)
        {
            if (_isEarlier)
            {
                _date -= timeSpan;
            }
            else
            {
                _date += timeSpan;
            }
            return this;
        }
        
        public DateTimeBuilder And()
        {
            return this;
        }
        
        public static implicit operator DateTime(DateTimeBuilder builder)
        {
            return builder._date;
        }
    }


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
            var date = new DateTime(2018, 07, 11, 23, 00, 00);
            
            var notExpiringOrder = Create.Order().CreatedOnDate(WhichIs.EarlierThan(date).For(1.Hour())).Please();
            var expiringOrder = Create.Order().CreatedOnDate(WhichIs.EarlierThan(date).For(1.Hour()).And().For(1.Minute())).Please();
           

            var trackerClient = Create.TrackerClient().With(expiringOrder).And().With(notExpiringOrder);
            var receivedOrders = Get.OrdersFrom(trackerClient).ExpiringOn(date).Please();

            AssertThat.The(receivedOrders).EqualTo(expiringOrder);
        }


        [Fact]
        public void CallGetOrdersOnOrdersProvider_WhenGetOrdersIsCalled()
        {
            var ordersProviderMock = new Mock<IOrdersProvider>();
            var dateProviderDummy = new DateProvider();
            var trackerClient = new TrackerClient(ordersProviderMock.Object, dateProviderDummy);

            var orders = GetOrdersWithoutExpiringOnlyParameter(trackerClient);

            ordersProviderMock.Verify(op => op.GetOrders(), Times.Once);
        }

        [Fact]
        public void NotCallNowOnDateProvider_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var ordersProviderStub = new OrdersProvider();
            var dateProviderMock = new Mock<IDateProvider>();
            var trackerClient = new TrackerClient(ordersProviderStub, dateProviderMock.Object);

            var orders = GetOrdersWithoutExpiringOnlyParameter(trackerClient);

            dateProviderMock.Verify(dp => dp.Now(), Times.Never);
        }

        [Fact]
        public void CallNowOnDateProvider_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var ordersProviderStub = new OrdersProvider();
            var dateProviderMock = new Mock<IDateProvider>();
            var trackerClient = new TrackerClient(ordersProviderStub, dateProviderMock.Object);

            var orders = GetOrdersWithExpiringOnlyParameterEqualToTrue(trackerClient);

            dateProviderMock.Verify(dp => dp.Now(), Times.Once);
        }

        [Fact]
        public void NotCallIsExpiringOnEachProductionOrder_WhenGetOrdersIsCalledWithoutExpiringOnlyParameter()
        {
            var productionOrderMock = new Mock<ProductionOrder>();
            var expectedOrders = new ProductionOrder[]
            {
                productionOrderMock.Object
            };
            var ordersProviderStub = new Mock<IOrdersProvider>();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            var dateProviderDummy = new DateProvider();
            var trackerClient = new TrackerClient(ordersProviderStub.Object, dateProviderDummy);

            var orders = GetOrdersWithoutExpiringOnlyParameter(trackerClient);

            productionOrderMock.Verify(pom => pom.IsExpiring(It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public void CallIsExpiringOnEachProductionOrder_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            var productionOrderMock = new Mock<ProductionOrder>();
            var expectedOrders = new ProductionOrder[]
            {
                productionOrderMock.Object
            };
            var ordersProviderStub = new Mock<IOrdersProvider>();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            var dateProviderStub = new DateProvider();
            var trackerClient = new TrackerClient(ordersProviderStub.Object, dateProviderStub);

            var orders = GetOrdersWithExpiringOnlyParameterEqualToTrue(trackerClient);

            productionOrderMock.Verify(pom => pom.IsExpiring(It.IsAny<DateTime>()), Times.Once);
        }


        private ProductionOrder[] GetOrdersWithoutExpiringOnlyParameter(ITrackerClient trackerClient)
        {
            return trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0);
        }

        private ProductionOrder[] GetOrdersWithExpiringOnlyParameterEqualToTrue(ITrackerClient trackerClient)
        {
            return trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0, true);
        }
    }

  
}

    