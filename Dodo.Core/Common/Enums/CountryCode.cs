namespace Dodo.Core.Common.Enums
{
	// Для двухбуквенных обозначений страны используются буквенные обозначения
	// из Общероссийского классификатора стран мира
	// https://ru.wikipedia.org/wiki/Общероссийский_классификатор_стран_мира
	public enum CountryCode
	{
		Ru = 643,
		Ro = 642,

		/// <summary>
		/// Казахстан
		/// </summary>
		Kz = 398, //код страны по ISO KZ кода Kk не существует в природе. KK это культура Казахстана
		Zh = 156,
		Uz = 860,
		Lt = 440,

		/// <summary>
		/// Estonia
		/// </summary>
		Ee = 233,//код страны по ISO EE Et - это Эфиопия бро, Вот культура эстонская это et.

		/// <summary>
		/// USA
		/// </summary>
		Us = 840,

		/// <summary>
		///  Кыргызстан
		/// </summary>
		Kg = 417,//код страны по ISO Kg Ky - это Каймановы острова 

		/// <summary>
		/// Китай
		/// </summary>
		Cn = 156,

		/// <summary>
		/// Великобритания
		/// </summary>
		Gb = 826
	}
}