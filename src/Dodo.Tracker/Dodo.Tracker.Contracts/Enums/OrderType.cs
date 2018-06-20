using System.Runtime.Serialization;

namespace Dodo.Tracker.Contracts.Enums
{
	/// <summary>
	/// Order's source. Ex: Pizzeria, Mobile, Site & etc.
	/// </summary>
	[DataContract]
	public enum OrderType
	{
		[EnumMember] Unknown = 0,
		[EnumMember] Delivery = 1,
		[EnumMember] Pickup = 2,
		[EnumMember] Stationary = 3,
		[EnumMember] PersonalFood = 4,
		[EnumMember] ShopWindowSupply = 5
	}
}
