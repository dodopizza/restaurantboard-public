using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;

namespace Dodo.Tests.DSL
{
    public class OrderStorageBuilder
    {
        private IDateTimeProvider _dateTimeProvider;

        public OrderStorageBuilder()
        {
            SetDefault();
        }

        public OrderStorageBuilder Default()
        {
            SetDefault();
            return this;
        }

        public OrderStorageBuilder WithDateTimeProvider(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            return this;
        }

        public IOrdersStorage RightNow()
        {
            return new InMemoryOrdersStorage(_dateTimeProvider);
        }

        private void SetDefault()
        {
            _dateTimeProvider = Gimme.DefaultDateTimeProvider();
        }
    }
}
