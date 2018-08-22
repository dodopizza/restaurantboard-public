using Dodo.RestaurantBoard.Tests.DSL;
using System.Linq;
using Xunit;

namespace Dodo.RestaurantBoard.Tests
{
    public class PizzeriaOrdersServiceTest
    {
        [Fact]
        public void GetOrders_ShouldReturnOrderForPupaClientName()
        {
            var trackerClient = Create.TrackerClient.WithOrderFrom("Пупа").Please();
            var pizzeriaOrdersService = Create.PizzeriaOrdersService.With(trackerClient).Please();
            var pizzeria = Create.Pizzeria.Please();

            var orders = pizzeriaOrdersService.GetOrders(pizzeria).Result;

            Assert.Single(orders);
            Assert.Equal("Пупа", orders.First().ClientName);
        }
    }
}