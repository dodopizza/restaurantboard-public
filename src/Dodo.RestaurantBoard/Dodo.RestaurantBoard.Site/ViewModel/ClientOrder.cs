using System;
using System.Runtime.Serialization;

namespace Dodo.RestaurantBoard.Site.ViewModel
{
    [Serializable]
    [DataContract]
    public class ClientOrder
    {
        [DataMember]
        public Int32 OrderId { get; set; }
        [DataMember]
        public Int32 OrderNumber { get; set; }
        [DataMember]
        public String ClientName { get; set; }
        [DataMember]
        public string ClientIconPath { get; set; }
        [DataMember]
        public long OrderReadyTimestamp { get; set; }
        [DataMember]
        public string OrderReadyDateTime { get; set; }
    }
}
