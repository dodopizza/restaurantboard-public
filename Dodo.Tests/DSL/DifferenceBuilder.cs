using System;
using Xunit;

namespace Dodo.Tests.DSL
{
    public class DifferenceBuilder
    {
        private DateTime _dateTimeBefore;
        private DateTime _dateTimeAfter;

        public DifferenceBuilder(DateTime dateTime)
        {
            _dateTimeBefore = dateTime;
        }

        public DifferenceBuilder And(DateTime dateTime)
        {
            _dateTimeAfter = dateTime;
            return this;
        }

        public void ShouldBe(TimeSpan timeSpan)
        {
            Assert.Equal(_dateTimeAfter - _dateTimeBefore, timeSpan);
        }
    }
}