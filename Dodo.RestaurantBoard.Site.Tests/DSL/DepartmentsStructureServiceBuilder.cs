using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.DSL
{
    public class DepartmentsStructureServiceBuilder
    {
        private readonly Mock<IDepartmentsStructureService> _service;

        public DepartmentsStructureServiceBuilder()
        {
            _service = new Mock<IDepartmentsStructureService>();
        }

        public DepartmentsStructureServiceBuilder With(Pizzeria pizzeria)
        {
            _service
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(pizzeria);

            return this;
        }

        public IDepartmentsStructureService Please()
        {
            return _service.Object;
        }
    }
}