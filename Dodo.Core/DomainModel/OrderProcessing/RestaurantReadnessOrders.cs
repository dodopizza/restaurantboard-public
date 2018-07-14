using System;

namespace Dodo.Core.DomainModel.OrderProcessing
{
	public sealed class RestaurantReadnessOrders
	{
		public Int32 OrderId { get; }
	    public bool IsExpired { get; set; }
		public Int32 OrderNumber { get; }
		public String ClientName { get; }		
		public DateTime OrderReadyDateTime { get; }

		public RestaurantReadnessOrders(Int32 orderId, Int32 orderNumber, String clientName, DateTime orderReadyDateTime, bool isExpired)
		{
			OrderId = orderId;
			ClientName = clientName;
			OrderNumber = orderNumber;
			OrderReadyDateTime = orderReadyDateTime;
		    IsExpired = isExpired;
		}
	}
}
