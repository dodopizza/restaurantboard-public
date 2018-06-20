namespace Dodo.Core.DomainModel.Management.Organizations
{
	/// <summary>
	/// Типы организаций. У каждой страны зарезервировано 10 цифр. Если не хватает, забираем новые 10 цифр.
	/// У каждой страны добавлять форму, даже если они совпадают с формой другой страны.
	/// </summary>
	public enum OrganizationType
	{
		// ru
		Rus_IP = 0,     //Россия - ИП; Великобритания - Sole Trader
		Rus_OOO = 1,
		Rus_OAO = 2,
		Rus_ZAO = 3,

		// ro
		Ro_II = 10,
		Ro_SA = 11,
		Ro_SRL = 12,

		// kz
		Kz_IP = 20,
		Kz_AO = 21,
		Kz_TOO = 22,

		// en
		Usa_LLC = 30,
		Usa_LLP = 31,
		Usa_Ltd = 32,
		Usa_SP = 33,
		Usa_INC = 34,
		Usa_CORP = 35,

		// lt
		Lt_UAB = 40,    // Uždaroji akcinė bendrovė
		Lt_IL = 41, // IĮ - Individuali įmonė
		Lt_MB = 42, // Mažoji bendrija
		Lt_AB = 43,	// Akcinė bendrovė

		//uz
		Uz_OOO = 50,
		Uz_CHP = 51,	// Частный предприниматель
		Uz_CHF = 52,	// Частная фирма

		//ky
		Ky_IP = 60,
		Ky_OsOO = 61,	// ОсОО Общество с ограниченной ответственностью

		//ee
		Ee_AS = 70,	// AS (Aktsiaselts) - акционерное общество
		Ee_OU = 71,	// OÜ (Osaühing) - товарищество
		Ee_FIE = 72,	// FIE (Füüsiline isik ettevõtja) - физ лицо - предприниматель

		//gb
		Gb_IP = 80,
		Gb_LLP = 81,
		Gb_Ltd = 82,
		Gb_PLC = 83,    //PLC = Public Limited Company - Открытая компания с ограниченной ответственностью
	}
}