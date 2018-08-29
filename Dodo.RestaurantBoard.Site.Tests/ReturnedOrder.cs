using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.RestaurantBoard.Site.Tests
{
    public class ReturnedOrder
    {
        public int OrderId { get; set; }

        public int OrderNumber { get; set; }

        public string ClientName { get; set; }

        public string ClientIconPath { get; set; }

        public long OrderReadyTimestamp { get; set; }

        public string OrderReadyDateTime { get; set; }
    }
}
