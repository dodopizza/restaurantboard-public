using System.Runtime.Serialization;

namespace Dodo.Tracker.Contracts.Enums
{
	[DataContract]
	public enum OrderState
	{
		[EnumMember] Unknown = -1,
		[EnumMember] PreOrder = 0,

		/// <summary>
		/// Принят
		/// </summary>
		[EnumMember] Accepted = 1,

		/// <summary>
		/// Кухня (статус появляется тогда, когда пиццамейкером принято в работу хотя бы одно блюдо из заказа)
		/// </summary>
		[EnumMember] Kitchen = 2,

		/// <summary>
		/// На полке (заказ готов, ожидает доставки)
		/// </summary>
		[EnumMember] OnTheShelf = 3,

		/// <summary>
		/// На доставке (заказ принят курьером, но еще нет информации, что он доставлен)
		/// </summary>
		[EnumMember] OnDelivery = 4,

		/// <summary>
		/// Заказ выполнен
		/// </summary>
		[EnumMember] Completed = 5,

		/// <summary>
		/// Заказ перенаправлен
		/// </summary>
		[EnumMember] Redirect = 6,


		/// <summary>
		/// сторно заказа
		/// </summary>
		[EnumMember] Storno = 10,

		/// <summary>
		/// просрочен
		/// </summary>
		[EnumMember] TimeOut = 11,

		/// <summary>
		/// отказ от заказа
		/// </summary>
		[EnumMember] Failure = 12,

		[EnumMember] Editing = 200
	}
}
