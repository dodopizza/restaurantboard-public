using System.Collections.Generic;

namespace Dodo.RestaurantBoard.Site.ViewModels
{
    public interface IOrderReadinessResult
    {
        int PlayTune { get; set; }
        bool NewOrderArrived { get; set; }
        string SongName { get; set; }
        List<IClientOrder> ClientOrders { get; set; }
    }
}