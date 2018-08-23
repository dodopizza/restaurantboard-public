namespace Dodo.RestaurantBoard.Site.ViewModels
{
    public interface IClientOrder
    {
        int OrderId { get; set; }
        int OrderNumber { get; set; }
        string ClientName { get; set; }
        string ClientIconPath { get; set; }
        long OrderReadyTimestamp { get; set; }
        string OrderReadyDateTime { get; set; }
    }
}