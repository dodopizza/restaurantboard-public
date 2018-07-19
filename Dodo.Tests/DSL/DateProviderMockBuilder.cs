using System;
using Dodo.RestaurantBoard.Domain.Services;
using Moq;

namespace Dodo.Tests.DSL
{
    public class DateProviderMockBuilder
    {
        private readonly Mock<IDateProvider> _dateProvider;
            
        public DateProviderMockBuilder()
        {
            _dateProvider = new Mock<IDateProvider>();
        }

        internal DateProviderMockBuilder WithNowAs(DateTime dateTime)
        {
            _dateProvider.Setup(_ => _.Now()).Returns(dateTime);

            return this;
        }

        internal Mock<IDateProvider> Please()
        {
            return _dateProvider;
        }

        public static implicit operator Mock<IDateProvider>(DateProviderMockBuilder builder)
        {
            return builder._dateProvider;
        }
    }
}
