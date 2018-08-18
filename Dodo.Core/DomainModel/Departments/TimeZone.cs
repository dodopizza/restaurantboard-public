using System;

namespace Dodo.Core.DomainModel.Departments
{
    [Obsolete("Use TimeZoneNew")]
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

        public short TimeZoneShift()
        {
            var currentTimeZoneUTCOffset = _utcOffsetProvider.GetUtcOffset().TotalMinutes;
            return (short)Math.Round(((Double)_timeZoneUTCOffset - currentTimeZoneUTCOffset) / 60);
        }
        
        public string TimeZoneUTCOffsetString()
        {
            char mathSimbol;

            if (_timeZoneUTCOffset > 0)
                mathSimbol = '+';
            else if (TimeZoneShift() < 0)
                mathSimbol = '-';
            else
                mathSimbol = ' ';

            var timeSpan = TimeSpan.FromMinutes(Math.Abs(_timeZoneUTCOffset));
            var fromTimeString = timeSpan.ToString(@"hh\:mm");

            return String.Format("{0}{1}", mathSimbol, fromTimeString);
        }

        public string TimeZoneShiftString()
        {
            var shift = TimeZoneShift();

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
