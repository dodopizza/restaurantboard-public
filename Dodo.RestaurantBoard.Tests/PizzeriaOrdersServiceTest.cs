using System.Linq;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
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

    public class Create
    {
        public static TrackerBuilder TrackerClient => new TrackerBuilder();
        public static PizzeriaOrdersServiceBuilder PizzeriaOrdersService => new PizzeriaOrdersServiceBuilder();
        public static PizzeriaBuilder Pizzeria => new PizzeriaBuilder();
    }

    public class PizzeriaBuilder
    {
        private Pizzeria _pizzeria;
        
        public Pizzeria Please()
        {
            throw new System.NotImplementedException();
        }
    }

    public class PizzeriaOrdersServiceBuilder
    {
        private IPizzeriaOrdersService _pizzeriaOrdersService;
        
        public PizzeriaOrdersServiceBuilder With(object trackerClient)
        {
            return this;
        }

        public IPizzeriaOrdersService Please()
        {
            throw new System.NotImplementedException();
        }
    }

    public class TrackerBuilder
    {
        private ITrackerClient _trackerClient;
        
        public TrackerBuilder WithOrderFrom(string пупа)
        {
            return this;
        }

        public ITrackerClient Please()
        {
            throw new System.NotImplementedException();
        }
    }
}