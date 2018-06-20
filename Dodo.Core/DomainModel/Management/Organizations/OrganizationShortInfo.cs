using System;

namespace Dodo.Core.DomainModel.Management.Organizations
{
	[Serializable]
	public class OrganizationShortInfo
	{
		public Int32 Id { get; private set; }
		public String NameFull { get; private set; }
		public String NameShort { get; private set; }
		public OrganizationType? Type { get; private set; }
		public String Address { get; private set; }
		public String PozitionOfHead { get; private set; }
		public String HeadManagerName { get; private set; }
		public Int32 CountryId { get; private set; }
		public String BankName { get; private set; }
		public String CheckingAccount { get; private set; }
		public String ShareCapital { get; private set; }


		public OrganizationShortInfo
		(
			Int32 id, 
			String nameFull, 
			String nameShort, 
			OrganizationType? type, 
			String address, 
			String pozitionOfHead, 
			String headManagerName, 
			Int32 countryId,
			String bankName,
			String checkingAccount,
			String shareCapital
		)
		{
			Id = id;
			NameFull = nameFull;
			NameShort = nameShort;
			Type = type;
			Address = address;
			PozitionOfHead = pozitionOfHead;
			HeadManagerName = headManagerName ?? string.Empty;
			CountryId = countryId;
			BankName = bankName;
			CheckingAccount = checkingAccount;
			ShareCapital = shareCapital;
		}
	}
}