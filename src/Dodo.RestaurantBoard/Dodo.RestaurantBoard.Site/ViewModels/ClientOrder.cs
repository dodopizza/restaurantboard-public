using System;
using System.Runtime.Serialization;

namespace Dodo.RestaurantBoard.Site.ViewModels
{
    [Serializable]
    [DataContract]
    public class ClientOrder : IClientOrder
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