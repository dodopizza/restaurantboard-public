using System;
using System.Threading;
using Dodo.Core.Common;
using Dodo.Core.Common.Enums;
using Dodo.Core.DomainModel.Departments;

namespace Dodo.Core.DomainModel.Management.Organizations
{
	[Serializable]
	public abstract class Organization
	{
		public Int32 Id { get; private set; }
		public Uuid Uuid { get; private set; }
		public String NameFull { get; private set; }
		public String NameShort { get; private set; }
		public String Address { get; private set; }
		public String PozitionOfHead { get; private set; }
		public String HeadManagerName { get; private set; }
		public Country Country { get; private set; }
		public String BankName { get; private set; }
		public String CheckingAccount { get; private set; }
		public String ShareCapital { get; private set; }

		public Organization
		(
			Int32 id,
			Uuid uuid,
			String nameFull,
			String nameShort,
			String address,
			String pozitionOfHead,
			String headManagerName,
			Country country,
			String bankName,
			String checkingAccount,
			String shareCapital
		)
		{
			Id = id;
			Uuid = uuid;
			NameFull = nameFull;
			NameShort = nameShort;
			Address = address;
			PozitionOfHead = pozitionOfHead;
			HeadManagerName = headManagerName;
			Country = country;
			BankName = bankName;
			CheckingAccount = checkingAccount;
			ShareCapital = shareCapital;
		}

		public abstract String GetShortInfo();
		public abstract String GetFullDescription();
		public abstract String GetShortDescription();
		public abstract OrganizationShortInfo OrganizationShortInfo { get; }

	    public virtual string GetTextForAgreement(string text)
	    {
	        return text;
	    }

		public String ShortHeadManagerName
		{
			get
			{
				Char[] separator = {' ', '.'};
				String[] parts = Thread.CurrentThread
					.CurrentCulture.TextInfo
					.ToTitleCase(HeadManagerName)
					.Split(separator, StringSplitOptions.RemoveEmptyEntries);

				if (parts.Length > 0) parts[0] += " ";

				for (Int32 i = 1; i < parts.Length; i++) parts[i] = parts[i][0] + ".";

				return String.Join(String.Empty, parts);
			}
		}
	}
}