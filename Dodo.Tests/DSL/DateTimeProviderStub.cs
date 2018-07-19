using Dodo.Core.Services;
using Moq;
using System;

namespace Dodo.Tests.DSL
{
    public class DateTimeProviderStub
    {
        private Mock<IDateTimeProvider> _dateTimeProviderStub;

        public DateTimeProviderStub()
        {
            _dateTimeProviderStub = new Mock<IDateTimeProvider>();
        }

        public DateTimeProviderStub(DateTime date) : this()
        {
            _dateTimeProviderStub.Setup(p => p.GetDateTime()).Returns(date);
        }

        public DateTimeProviderStub WithDate(DateTime date)
        {
            _dateTimeProviderStub.Setup(p => p.GetDateTime()).Returns(date);
            return this;
        }

        public DateTimeProviderStub WithDates(params DateTime[] dates)
        {
            var result = _dateTimeProviderStub.SetupSequence(p => p.GetDateTime());
            foreach(var date in dates)
            {
                result = result.Returns(date);
            }
            return this;
        }

        public IDateTimeProvider RightNow()
        {
            return _dateTimeProviderStub.Object;
        }
    }
}