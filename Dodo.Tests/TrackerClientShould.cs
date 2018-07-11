using Dodo.RestaurantBoard.Domain.Services;
using Xunit;

namespace Dodo.Tests
{
    public class TrackerClientShould
    {
        [Fact]
        public void ReturnAllOrders_IfIsExpiringParameterNotSpecifiedAndDefaultValueIsUsed()
        {
            var trackerClient = new TrackerClient();
        }
    }
}