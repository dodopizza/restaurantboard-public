using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Dodo.RestaurantBoard.Site.ViewModels
{
    [Serializable]
    [DataContract]
    public class OrderReadinessResult : IOrderReadinessResult
    {
        [DataMember]
        public int PlayTune { get; set; }

        [DataMember]
        public bool NewOrderArrived { get; set; }

        [DataMember]
        public string SongName { get; set; }

        [DataMember]
        public List<IClientOrder> ClientOrders { get; set; }
    }
}