using System;
using System.Runtime.Serialization;

namespace Dodo.RestaurantBoard.Site.ViewModel
{
    [DataContract]
    [Serializable]
    public class Board
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
