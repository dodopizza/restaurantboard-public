using System;

namespace Dodo.Core.DomainModel.Localization
{
	[Serializable]
    public class Cultures
    {
        public String CultureName { private set; get; }
        public String ShortName { private set; get; }

        public Boolean IsFilled { set; get; }
        public Boolean IsCurrent { set; get; }

        public Boolean IsNative { private set; get; }

        public Cultures(String cultureName, String shortName, Boolean isNative)
        {
            CultureName = cultureName;
            ShortName = shortName;
            IsNative = isNative;
        }
    }
}
