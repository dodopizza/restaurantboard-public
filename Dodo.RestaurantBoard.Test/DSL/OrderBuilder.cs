using System;
using Dodo.Tracker.Contracts;

namespace Dodo.RestaurantBoard.Test.DSL
{
    public class OrderBuilder
    {
        private string _clientName;
        private DateTime _orderDate;
        private int _id;
        private int _number;

        public OrderBuilder For(string clientName)
        {
            _clientName = clientName;
            return this;
        }

        public OrderBuilder WithDate(DateTime orderDate)
        {
            _orderDate = orderDate;
            return this;
        }

        public OrderBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public OrderBuilder WithNumber(int number)
        {
            _number = number;
            return this;
        }

        public ProductionOrder Please()
        {
            return new ProductionOrder
            {
                ChangeDate = _orderDate,
                ClientName = _clientName,
                Id = _id,
                Number = _number
            };
        }
    }
}