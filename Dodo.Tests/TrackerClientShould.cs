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
        public static GetOrdersCallBuilder OrdersFrom(ITrackerClient trackerClient)
        {
            return new GetOrdersCallBuilder(trackerClient);
        }       

        //public static GetOrdersCallBuilder ExpiringOrdersFrom(ITrackerClient trackerClient)
        //{
        //    return new GetOrdersCallBuilder(trackerClient, true);
        //}
    }

    public class GetOrdersCallBuilder
    {
        private readonly ITrackerClient _trackerClient;  
        private bool _expiringOnly;

        public GetOrdersCallBuilder(ITrackerClient client)
        {
            _trackerClient = client;
        }
        
        public ProductionOrder[] Please()
        {
            return _trackerClient.GetOrders(new Uuid(), OrderType.Delivery, new OrderState[1], 0, _expiringOnly);
        }

        internal object ExpiringOn(DateTime date)
        {
            _expiringOnly = true;
            
        }
    }

    public class TrackerClientBuilder
    {
        private List<ProductionOrder> _orders = new List<ProductionOrder>();

        public TrackerClientBuilder WithOrders(ProductionOrder[] productionOrders)
        {
            _orders.AddRange(productionOrders);
            return this;
        }        

        public TrackerClientBuilder With(ProductionOrder order)
        {
            _orders.Add(order);
            return this;
        }

        public TrackerClientBuilder And()
        {
            return this;
        }
        
        public TrackerClient Please()
        {
            var ordersProviderStub = new Mock<IOrdersProvider>();
            var dateProviderDummy = new DateProvider();
            ordersProviderStub.Setup(p => p.GetOrders()).Returns(_orders.ToArray());
            return new TrackerClient(ordersProviderStub.Object, dateProviderDummy);
        }
    }

    public static class AssertThat
    {
       public static AssertBuilder The(ProductionOrder[] orders)
        {
            return new AssertBuilder(orders);
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
    }

    public static class WhichIs
    {        
        internal static DateTimeBuilder EarlierThan(DateTime date)
        {
            return new DateTimeBuilder(date);
        }
    }

    class DateTimeBuilder
    {
        private DateTime _date;

        public DateTimeBuilder(DateTime initialDate)
        {
            _date = initialDate;            
        }

        public DateTimeBuilder For(TimeSpan timeSpan)
        {
            _date += timeSpan;
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
            var trackerClient = Create.TrackerClient().WithOrders(orders);

            var receivedOrders = Get.OrdersFrom(trackerClient).Please();

            AssertThat.The(receivedOrders).EqualTo(orders);
        }

        [Fact]
        public void ReturnOnlyExpiringOrders_WhenGetOrdersIsCalledWithExpiringOnlyParameterEqualToTrue()
        {
            //            var dateProviderStub = new Mock<IDateProvider>();
            //            dateProviderStub.Setup(p => p.Now()).Returns();

            //
            var date = new DateTime(2018, 07, 11, 23, 00, 00);
            
            var expiringOrder = Create.Order().CreatedOnDate(WhichIs.EarlierThan(date).For(1.Hour())).Please();
            var notExpiringOrder = Create.Order().CreatedOnDate(WhichIs.EarlierThan(date).For(1.Hour()).And().For(1.Minute())).Please();

            //            var notExpiringOrder = new ProductionOrder
            //            {
            //                ChangeDate = new DateTime(2018, 07, 11, 22, 00, 00)
            //            };
            //            var expiringOrder = new ProductionOrder
            //            {
            //                ChangeDate = new DateTime(2018, 07, 11, 21, 59, 00)
            //            };
            //            var expectedOrders = new ProductionOrder[]
            //            {
            //                notExpiringOrder,
            //                expiringOrder
            //            };


            //            var ordersProviderStub = new Mock<IOrdersProvider>();
            //            ordersProviderStub.Setup(p => p.GetOrders()).Returns(expectedOrders);
            //            var trackerClient = new TrackerClient(ordersProviderStub.Object, dateProviderStub.Object);

            var trackerClient = Create.TrackerClient().With(expiringOrder).And().With(notExpiringOrder);

            //            var actualOrders = GetOrdersWithExpiringOnlyParameterEqualToTrue(trackerClient);
            var receivedOrders = Get.OrdersFrom(trackerClient).ExpiringOn(date).Please();

//            Assert.Equal(new[] { expiringOrder }, actualOrders);
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

    