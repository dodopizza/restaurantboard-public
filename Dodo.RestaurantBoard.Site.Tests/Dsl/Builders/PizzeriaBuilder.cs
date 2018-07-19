using Dodo.Core.DomainModel.Departments.Units;
using Dodo.RestaurantBoard.Site.Tests.Factories;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Builders
{
    public class PizzeriaBuilder
    {
        private ClientTreatment _clientTreatment;

        public PizzeriaBuilder WithClientTreatmentAsRandomImage()
        {
            _clientTreatment = ClientTreatment.RandomImage;
            return this;
        }

        public Pizzeria Please()
        {
            return PizzeriaFactory.CreatePizzeria(clientTreatment: _clientTreatment);
        }
    }
}