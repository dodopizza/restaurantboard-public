using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Dodo.RestaurantBoard.Domain.Services
{
    [Serializable]
    [DataContract]
    public class ClientOrder
    {
        [DataMember]
        public int OrderId { get; set; }
        [DataMember]
        public int OrderNumber { get; set; }
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string ClientIconPath { get; set; }
        [DataMember]
        public long OrderReadyTimestamp { get; set; }
        [DataMember]
        public string OrderReadyDateTime { get; set; }
    }
}
