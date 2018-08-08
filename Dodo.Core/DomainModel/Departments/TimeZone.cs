using System;

namespace Dodo.Core.DomainModel.Departments
{
    public class TimeZone
    {
        private UtcOffsetProvider _utcOffsetProvider;
        
        public TimeZone(UtcOffsetProvider utcOffsetProvider)
        {
            _utcOffsetProvider = utcOffsetProvider;
        }

        public Int16 TimeZoneShift(Int32 utcOffset)
        {
            Double currentTimeZoneUTCOffset = _utcOffsetProvider.GetUtcOffset().TotalMinutes;
            return (Int16)Math.Round(((Double)utcOffset - currentTimeZoneUTCOffset) / 60);
        }
        
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

        public static string TimeZoneShiftString(int shift)
        {
            Char mathSimbol;
            if (shift > 0)
                mathSimbol = '+';
            else if (shift < 0)
                mathSimbol = '-';
            else
                mathSimbol = ' ';

            return String.Format("{0}{1}", mathSimbol, Math.Abs(shift));
        } 
    }
}
