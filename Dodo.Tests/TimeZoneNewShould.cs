﻿using Dodo.Core.New.DomainModel.Departments;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dodo.Tests
{
    public class TimeZoneNewShould
    {
        [Fact]
        public void NegativeTimeZoneMainOfficeOffsetString_WhenMainOfficeTimeZoneUTCOffsetNegative()
        {
            var timeZone = new TimeZoneNew(-180);

            var offsetString = timeZone.TimeZoneMainOfficeOffsetString();

            Assert.Equal("-03:00", offsetString);
        }
    }
}