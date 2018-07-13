using System;
using System.Threading;
using Dodo.Core.Common;
using Dodo.Core.Common.Enums;
using Dodo.Core.DomainModel.Departments;

namespace Dodo.Core.DomainModel.Management.Organizations
{
    [Serializable]
    public class Organization
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

        public Organization(string headManagerName)
        {
            HeadManagerName = headManagerName;    
        }

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


        public static OrganizationType[] GetAvailableTypes(CountryCode countryCode)
        {
            switch (countryCode)
            {
                case CountryCode.Ru:
                    return new[]
                    {
                        OrganizationType.Rus_IP, OrganizationType.Rus_OAO, OrganizationType.Rus_OOO,
                        OrganizationType.Rus_ZAO
                    };
                case CountryCode.Ro:
                    return new[] {OrganizationType.Ro_II, OrganizationType.Ro_SA, OrganizationType.Ro_SRL};
                case CountryCode.Kz:
                    return new[] {OrganizationType.Kz_IP, OrganizationType.Kz_TOO, OrganizationType.Kz_AO};
                case CountryCode.Us:
                    return new[]
                    {
                        OrganizationType.Usa_LLC, OrganizationType.Usa_LLP, OrganizationType.Usa_Ltd,
                        OrganizationType.Usa_SP, OrganizationType.Usa_INC, OrganizationType.Usa_CORP
                    };
                case CountryCode.Zh:
                    return new OrganizationType[0];
                case CountryCode.Lt:
                    return new[]
                    {
                        OrganizationType.Lt_UAB, OrganizationType.Lt_IL, OrganizationType.Lt_MB, OrganizationType.Lt_AB
                    };
                case CountryCode.Kg:
                    return new[] {OrganizationType.Ky_IP, OrganizationType.Ky_OsOO};
                case CountryCode.Uz:
                    return new[] {OrganizationType.Uz_OOO, OrganizationType.Uz_CHP, OrganizationType.Uz_CHF};
                case CountryCode.Ee:
                    return new[] {OrganizationType.Ee_AS, OrganizationType.Ee_OU, OrganizationType.Ee_FIE};
                case CountryCode.Gb:
                    return new[]
                    {
                        OrganizationType.Gb_Ltd, OrganizationType.Gb_LLP, OrganizationType.Gb_IP,
                        OrganizationType.Gb_PLC
                    };
                default:
                    return null;
            }
        }
    }
}