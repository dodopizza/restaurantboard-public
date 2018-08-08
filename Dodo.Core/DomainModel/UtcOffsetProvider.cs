using System;

namespace Dodo.Core.DomainModel
{
    public class UtcOffsetProvider
    {
        public virtual TimeSpan GetUtcOffset()
        {
            return TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);
        }
    }
}
