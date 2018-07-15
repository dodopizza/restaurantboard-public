using System.Collections.Generic;

namespace Dodo.Core.DomainModel.ClientOrders
{
	public class ClientOrdersModel
	{
		public int PlayTune { get; set; }

		public bool NewOrderArrived { get; set; }

		public string SongName { get; set; }

		public IList<Order> ClientOrders { get; set; }
	}
}