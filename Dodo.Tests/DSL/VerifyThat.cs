using Dodo.RestaurantBoard.Domain.Services;
using Moq;

namespace Dodo.Tests.DSL
{
    public static class VerifyThat
    {
        public static void GetOrdersCallOnceIn(Mock<IOrdersProvider> provider)
        {
            provider.Verify(op => op.GetOrders(), Times.Once);
        }

        public static void NowNeverCalledIn(Mock<IDateProvider> dateProvider)
        {
            dateProvider.Verify(dp => dp.Now(), Times.Never);
        }

        public static void NowCalledOnceIn(Mock<IDateProvider> dateProvider)
        {
            dateProvider.Verify(dp => dp.Now(), Times.Once);
        }
    }
}
