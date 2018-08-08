using System;

namespace Dodo.Core.DomainModel.Departments
{
    public class TimeZone
    {
        private UtcOffsetProvider _utcOffsetProvider;
        private int _timeZoneUTCOffset;

        public int TimeZoneUTCOffset => _timeZoneUTCOffset;

        public TimeZone(UtcOffsetProvider utcOffsetProvider, int timeZoneUTCOffset)
        {
            _utcOffsetProvider = utcOffsetProvider;
            _timeZoneUTCOffset = timeZoneUTCOffset;
        }

        public short TimeZoneShift(Int32 utcOffset)
        {
            var currentTimeZoneUTCOffset = _utcOffsetProvider.GetUtcOffset().TotalMinutes;
            return (short)Math.Round(((Double)utcOffset - currentTimeZoneUTCOffset) / 60);
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
