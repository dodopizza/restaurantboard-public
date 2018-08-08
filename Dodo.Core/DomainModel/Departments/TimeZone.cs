using System;

namespace Dodo.Core.DomainModel.Departments
{
    public class TimeZone
    {
        public static string TimeZoneUTCOffsetString(int utcOffset, int shift)
        {
            char mathSimbol;

            if (utcOffset > 0)
                mathSimbol = '+';
            else if (shift < 0)
                mathSimbol = '-';
            else
                mathSimbol = ' ';

            var timeSpan = TimeSpan.FromMinutes(Math.Abs(utcOffset));
            var fromTimeString = timeSpan.ToString(@"hh\:mm");

            return String.Format("{0}{1}", mathSimbol, fromTimeString);
        }
    }
}
