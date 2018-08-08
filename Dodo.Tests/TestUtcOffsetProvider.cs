using Dodo.Core.DomainModel;
using System;

namespace Dodo.Tests
{
    public class TestUtcOffsetProvider : UtcOffsetProvider
    {
        private int _offsetInHours;

        public TestUtcOffsetProvider(int offsetInHours)
        {
            _offsetInHours = offsetInHours;
        }

        public override TimeSpan GetUtcOffset()
        {
            return TimeSpan.FromHours(_offsetInHours);
        }
    }
}
