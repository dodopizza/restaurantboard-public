using System;
using System.Runtime.Serialization;

namespace Dodo.Core.DomainModel.OrderProcessing
{
    public interface IClientOrder
    {
        int OrderId { get; set; }
        int OrderNumber { get; set; }
        string ClientName { get; set; }
        string ClientIconPath { get; set; }
        long OrderReadyTimestamp { get; set; }
        string OrderReadyDateTime { get; set; }
        string Color { get; set; }
    }

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

        [DataMember]
        public string Color { get; set; }
    }
}