using System.Collections.Generic;

namespace Dodo.RestaurantBoard.Site.Models
{
    public class OrderReadiness
    {
        public OrderReadiness()
        {
            ClientOrders = new List<ClientOrders>();
        }

        public int PlayTune { get; set; }
        public bool NewOrderArrived { get; set; }
        public string SongName { get; set; }
        public ICollection<ClientOrders> ClientOrders { get; set; }
    }

    public class ClientOrders
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public string ClientName { get; set; }
        public string ClientIconPath { get; set; }
        public long OrderReadyTimestamp { get; set; }
        public string OrderReadyDateTime { get; set; }
    }
}
