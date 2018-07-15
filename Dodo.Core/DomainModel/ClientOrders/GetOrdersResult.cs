namespace Dodo.Core.DomainModel.ClientOrders
{
	public class GetOrdersResult
	{
		public ClientOrdersModel ClientOrdersModel { get; set; }

		public int[] ProductIds { get; set; }
	}
}