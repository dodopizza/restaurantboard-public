using System;
using System.Collections.Generic;
using System.Text;

namespace Dodo.Core.New.DomainModel.Departments
{
    public class TimeZoneNew
    {
        private int _mainOfficeTimeZoneUTCOffset;

        public TimeZoneNew(int mainOfficeTimeZoneUTCOffset)
        {
            _mainOfficeTimeZoneUTCOffset = mainOfficeTimeZoneUTCOffset;
        }
        public string TimeZoneMainOfficeOffsetString()
        {
            char mathSimbol;

            if (_mainOfficeTimeZoneUTCOffset < 0)
                mathSimbol = '-';
            else
                mathSimbol = ' ';

            var timeSpan = TimeSpan.FromMinutes(Math.Abs(_mainOfficeTimeZoneUTCOffset));
            var fromTimeString = timeSpan.ToString(@"hh\:mm");

            return String.Format("{0}{1}", mathSimbol, fromTimeString);
        }
    }
}
