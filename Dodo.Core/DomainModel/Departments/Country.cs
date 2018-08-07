using System;
using Dodo.Core.Common;
using Dodo.Core.Common.Enums;
using Dodo.Core.DomainModel.Finance;
using Dodo.Core.DomainModel.Localization;

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
	}
}