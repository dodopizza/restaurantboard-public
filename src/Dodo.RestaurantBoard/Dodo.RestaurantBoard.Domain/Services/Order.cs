using Dodo.Core.Services;
using Dodo.Tracker.Contracts;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Dodo.RestaurantBoard.Domain.Services
{
    [Serializable]
    [DataContract]
    public class Order
    {
        [DataMember]
        public int PlayTune { get; set; }
        [DataMember]
        public bool NewOrderArrived { get; set; }
        [DataMember]
        public string SongName { get; set; }
        [DataMember]
        public ClientOrder[] ClientOrders { get; set; }
    }

}
