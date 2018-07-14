namespace Dodo.Core.DomainModel.ClientOrders
{
	public class Order
	{
		public int OrderId { get; set; }
		public int OrderNumber { get; set; }
		public string ClientName { get; set; }
		public string ClientIconPath { get; set; }
		public long OrderReadyTimestamp { get; set; }
		public string OrderReadyDateTime { get; set; }
	}
}
