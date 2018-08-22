using System;
using System.Runtime.Serialization;

namespace Dodo.Core.DomainModel.OrderProcessing
{
    public interface IOrder
    {
        int PlayTune { get; set; }
        bool NewOrderArrived { get; set; }
        string SongName { get; set; }
        IClientOrder[] ClientOrders { get; set; }
    }

    [Serializable]
    [DataContract]
    public class Order : IOrder
    {
        [DataMember]
        public int PlayTune { get; set; }

        [DataMember]
        public bool NewOrderArrived { get; set; }

        [DataMember]
        public string SongName { get; set; }

        [DataMember]
        public IClientOrder[] ClientOrders { get; set; }
    }
}