using System;
using Dodo.Core.Common;
using Dodo.Core.Common.Enums;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.Localization;
using Dodo.Core.DomainModel.Management.Organizations;

namespace Dodo.Core.DomainModel.Departments
{
	[Serializable]
	public class Country
	{
		public Int32 Id { get; private set; }
		public String Name { get; private set; }
		public String PhoneCode { get; private set; }
		public Decimal? TaxRate { get; private set; }

		public String PhoneMask { get; private set; }

		public Cultures[] AvailableCultures { get; private set; }

		public Cultures NativeCulture
		{
			get
			{
				if (AvailableCultures != null)
				{
					foreach (var ac in AvailableCultures)
					{
						if (ac.IsNative)
							return ac;
					}
				}

				return null;
			}
		}

		public Currency Currency { get; private set; }

		/// <summary>
		/// CultureInfo name.
		/// </summary>
		public CountryCode Code => (CountryCode)Id;

		public Country(int id, string name, string phoneCode, decimal? taxRate, string phoneMask, Currency currency)
		{
			Id = id;
			Name = name;
			PhoneCode = phoneCode;
			TaxRate = taxRate;
			PhoneMask = phoneMask;
			Currency = currency;

			AvailableCultures = CultureConfigurationSectionElement.GetAvailableCultures();
		}

		public override String ToString() => Name;

		public OrganizationType[] GetAvailableTypes()
		{
			switch (Code)
			{
				case CountryCode.Ru:
					return new[] { OrganizationType.Rus_IP, OrganizationType.Rus_OAO, OrganizationType.Rus_OOO, OrganizationType.Rus_ZAO };
				case CountryCode.Ro:
					return new[] { OrganizationType.Ro_II, OrganizationType.Ro_SA, OrganizationType.Ro_SRL };
				case CountryCode.Kz:
					return new[] { OrganizationType.Kz_IP, OrganizationType.Kz_TOO, OrganizationType.Kz_AO };
				case CountryCode.Us:
					return new[] { OrganizationType.Usa_LLC, OrganizationType.Usa_LLP, OrganizationType.Usa_Ltd, OrganizationType.Usa_SP, OrganizationType.Usa_INC, OrganizationType.Usa_CORP };
				case CountryCode.Zh:
					return new OrganizationType[0];
				case CountryCode.Lt:
					return new[] {OrganizationType.Lt_UAB, OrganizationType.Lt_IL, OrganizationType.Lt_MB, OrganizationType.Lt_AB};
				case CountryCode.Kg:
					return new[] {OrganizationType.Ky_IP, OrganizationType.Ky_OsOO};
				case CountryCode.Uz:
					return new[] {OrganizationType.Uz_OOO, OrganizationType.Uz_CHP, OrganizationType.Uz_CHF};
				case CountryCode.Ee:
					return new []{OrganizationType.Ee_AS, OrganizationType.Ee_OU, OrganizationType.Ee_FIE};
				case CountryCode.Gb:
					return new[] { OrganizationType.Gb_Ltd, OrganizationType.Gb_LLP, OrganizationType.Gb_IP, OrganizationType.Gb_PLC };
				default:
					return null;
			}
		}
	}
}