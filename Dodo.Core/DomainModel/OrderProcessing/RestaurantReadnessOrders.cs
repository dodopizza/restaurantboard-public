using System;

namespace Dodo.Core.DomainModel.OrderProcessing
{
	public sealed class RestaurantReadnessOrders
	{
		public Int32 OrderId { get; }
		public Int32 OrderNumber { get; }
		public String ClientName { get; }		
		public DateTime OrderReadyDateTime { get; }

		public RestaurantReadnessOrders(Int32 orderId, Int32 orderNumber, String clientName, DateTime orderReadyDateTime)
		{
			OrderId = orderId;
			ClientName = clientName;
			OrderNumber = orderNumber;
			OrderReadyDateTime = orderReadyDateTime;
		}

		public string Color => OrderNumber % 2 == 0 ? "red" : "green";
	}
}
