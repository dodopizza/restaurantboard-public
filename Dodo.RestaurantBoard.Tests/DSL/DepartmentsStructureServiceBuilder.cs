using Dodo.Core.Common;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Domain.Services;
using Moq;

namespace Dodo.RestaurantBoard.Tests.DSL
{
    public class DepartmentsStructureServiceBuilder
    {
        private Mock<IDepartmentsStructureService> _departmentsStructureService;

        public DepartmentsStructureServiceBuilder()
        {
            _departmentsStructureService = new Mock<IDepartmentsStructureService>();
        }

        public DepartmentsStructureServiceBuilder WithPizzeria(Pizzeria pizzeria )
        {
            _departmentsStructureService.Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>())).Returns(pizzeria);
            return this;
        }

        public IDepartmentsStructureService Please()
        {
            return _departmentsStructureService.Object;
        }
    }
}