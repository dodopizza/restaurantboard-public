using Dodo.Core.DomainModel.Departments.Departments;
using Dodo.Core.DomainModel.Departments.Units;
using Dodo.Core.Services;
using Dodo.RestaurantBoard.Site.Tests.Factories;
using Moq;

namespace Dodo.RestaurantBoard.Site.Tests.Dsl.Builders
{
    public class DepartmentsStructureServiceMockBuilder
    {
        private Mock<IDepartmentsStructureService> _service;

        public DepartmentsStructureServiceMockBuilder()
        {
            _service = new Mock<IDepartmentsStructureService>();
        }

        public DepartmentsStructureServiceMockBuilder WithoutDepartments()
        {
            _service
                .Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(() => null);
            return this;
        }

        public DepartmentsStructureServiceMockBuilder WithDepartment(CityDepartment cityDepartment = null)
        {
            _service
                .Setup(x => x.GetDepartmentByUnitOrCache(It.IsAny<int>()))
                .Returns(() => cityDepartment ?? new CityDepartment());

            _service
                .Setup(x => x.GetDepartmentOrCache<CityDepartment>(It.IsAny<int>()))
                .Returns(() => cityDepartment ?? new CityDepartment());
            return this;
        }

        public DepartmentsStructureServiceMockBuilder WithDepartmentAndPizzeria()
        {
            var cityDepartment = Create.CityDepartment.WithContry().Please();
            var pizzeria = Create.Pizzeria.WithClientTreatmentAsRandomImage().Please();
            return Create.DepartmentsStructureService
                .WithDepartment(cityDepartment)
                .WithPizzeria(pizzeria);
        }

        public DepartmentsStructureServiceMockBuilder WithPizzeria(Pizzeria pizzeria = null)
        {
            _service
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(() => pizzeria ?? PizzeriaFactory.CreatePizzeria());
            return this;
        }

        public DepartmentsStructureServiceMockBuilder WithoutPizzeria()
        {

            _service
                .Setup(x => x.GetPizzeriaOrCache(It.IsAny<int>()))
                .Returns(() => null);
            return this;
        }

        public IDepartmentsStructureService Please()
        {
            return _service.Object;
        }

        public Mock<IDepartmentsStructureService> MockPlease()
        {
            return _service;
        }


    }
}