using System.Collections.Generic;
using System.Configuration;
using Dodo.Core.DomainModel.Localization;

namespace Dodo.Core.Common
{
    public class StartupSettingsCountryConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Cultures")]
        public CultureConfigurationCollection Sections
        {
            get { return (CultureConfigurationCollection)this["Cultures"]; }
        }
    }

    [ConfigurationCollection(typeof(CultureConfigurationSectionElement))]
    public class CultureConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new CultureConfigurationSectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CultureConfigurationSectionElement)(element)).Code;
        }

        public CultureConfigurationSectionElement this[int idx]
        {
            get { return (CultureConfigurationSectionElement)BaseGet(idx); }
        }
    }

    public class CultureConfigurationSectionElement : ConfigurationElement
    {
        [ConfigurationProperty("code", DefaultValue = "ru-RU", IsKey = true, IsRequired = true)]
        public string Code
        {
            get { return (string)this["code"]; }
            set { this["code"] = value; }
        }

        [ConfigurationProperty("shortName", DefaultValue = "ru", IsKey = false, IsRequired = true)]
        public string ShortName
        {
            get { return (string)this["shortName"]; }
            set { this["shortName"] = value; }
        }

        [ConfigurationProperty("isDefault", DefaultValue = "false", IsKey = false, IsRequired = true)]
        public bool IsDefault
        {
            get { return (bool)this["isDefault"]; }
            set { this["isDefault"] = value; }
        }

        public Cultures[] GetAvailableCultures()
        {
            try
            {
                var countrySettings = GetCountrySettings();

                if (countrySettings == null) return null;

                var cultureCodes = new List<Cultures>();
                for (int i = 0; i < countrySettings.Sections.Count; i++)
                {
                    cultureCodes.Add(
                        new Cultures(
                            countrySettings.Sections[i].Code,
                            countrySettings.Sections[i].ShortName,
                            countrySettings.Sections[i].IsDefault
                        ));
                }

                return cultureCodes.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public virtual StartupSettingsCountryConfigSection GetCountrySettings()
        {
            return (StartupSettingsCountryConfigSection)
                ConfigurationManager.GetSection("applicationSettings/Dodo.Settings.Country");
        }
    }
}