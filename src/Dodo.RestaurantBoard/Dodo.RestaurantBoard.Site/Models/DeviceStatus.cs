namespace Dodo.RestaurantBoard.Site.Models
{
	public class DeviceStatus
	{
		public bool IsDevice { get; set; }
		public bool IsDeviceRegistered { get; set; }
		public bool IsDeviceLoggedIn { get; set; }
		public bool? IsDefaultUrl { get; set; }
	}
}
